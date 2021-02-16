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
    public class SQLiteDatabase : ISQLiteDatabase
    {
        private readonly string name;
        private readonly string path;

        public SQLiteDatabase(string path, bool useCopy = true)
        {
            name = Path.GetFileNameWithoutExtension(path);
            path = path.Contains(Application.dataPath) ? path : $"{Application.streamingAssetsPath}/{path}";

#if UNITY_EDITOR
            this.path = path;
#else
            string copyPath = $"{Application.persistentDataPath}/{Path.GetFileNameWithoutExtension(path)} (Copy){Path.GetExtension(path)}";
            if (!File.Exists(copyPath))
            {
                byte[] bytes = File.ReadAllBytes(path);
                
                File.WriteAllBytes(copyPath, bytes);
            }

            this.path = useCopy ? copyPath : path;
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
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                TableQuery<T> tableQuery = sqliteConnection.Table<T>();

                if (predicate != null)
                    tableQuery = tableQuery.Where(predicate);

                return tableQuery.FirstOrDefault();
            }
        }

        public T[] GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : new()
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                TableQuery<T> tableQuery = sqliteConnection.Table<T>();

                if (predicate != null)
                    tableQuery = tableQuery.Where(predicate);

                return tableQuery.ToArray();
            }
        }

        public T Insert<T>(T data) where T : new()
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                sqliteConnection.CreateTable<T>();
                sqliteConnection.Insert(data);

                return data;
            }
        }

        public T InsertOrReplace<T>(T data) where T : new()
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                sqliteConnection.InsertOrReplace(data);

                return data;
            }
        }

        public IEnumerable<T> InsertAll<T>(IEnumerable<T> data) where T : new()
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                sqliteConnection.InsertAll(data);

                return data;
            }
        }

        public void DeleteAll<T>() where T : new()
        {
            using (SQLiteConnection sqliteConnection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
            {
                sqliteConnection.DeleteAll<T>();
            }
        }
    }
}