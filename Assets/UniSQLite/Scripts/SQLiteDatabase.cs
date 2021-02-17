using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using SQLite;
using UniSQLite.Assets;

namespace UniSQLite
{
    public class SQLiteDatabase : IDisposable
    {
        private readonly string name;

        public SQLiteConnection Connection { get; }

        public SQLiteDatabase(string path, SQLiteOpenFlags openFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create)
        {
            name = Path.GetFileNameWithoutExtension(path);

#if !UNITY_EDITOR
            string copyPath = $"{Application.persistentDataPath}/{Path.GetFileNameWithoutExtension(path)} (Copy){Path.GetExtension(path)}";
            if (!File.Exists(copyPath))
            {
                byte[] bytes = File.ReadAllBytes(path);
                
                File.WriteAllBytes(copyPath, bytes);
            }
            
            Connection = new SQLiteConnection(copyPath, openFlags);
#else
            path = path.Contains(Application.dataPath) ? path : $"{Application.streamingAssetsPath}/{path}";

            Connection = new SQLiteConnection(path, openFlags);
#endif
        }

        public void ShowTable<T>() where T : new()
        {
#if UNITY_EDITOR
            T[] tableRows = GetAll<T>();

            SQLiteTableAsset tableAssetScriptableObject = ScriptableObject.CreateInstance<SQLiteTableAsset>();
            tableAssetScriptableObject.name = typeof(T).Name;
            tableAssetScriptableObject.Initialize(tableRows, this);

            AssetDatabase.AddObjectToAsset(tableAssetScriptableObject, CreateAsset());
            AssetDatabase.SaveAssets();

            SQLiteDatabaseAsset CreateAsset()
            {
                SQLiteDatabaseAsset asset = ScriptableObject.CreateInstance<SQLiteDatabaseAsset>();

                AssetDatabase.CreateAsset(asset, $"Assets/{name}.asset");

                return asset;
            }
#endif
        }

        public T Get<T>(Expression<Func<T, bool>> predicate = null) where T : new()
        {
            TableQuery<T> tableQuery = Connection.Table<T>();

            if (predicate != null)
                tableQuery = tableQuery.Where(predicate);

            return tableQuery.FirstOrDefault();
        }

        public T[] GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : new()
        {
            TableQuery<T> tableQuery = Connection.Table<T>();

            if (predicate != null)
                tableQuery = tableQuery.Where(predicate);

            return tableQuery.ToArray();
        }

        public void UpdateAll(IEnumerable<object> rows)
        {
            Connection.UpdateAll(rows);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}