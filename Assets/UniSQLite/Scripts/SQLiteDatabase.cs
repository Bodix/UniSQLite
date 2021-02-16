using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using SQLite;
using UniSQLite.Assets;
using UniSQLite.Dispatcher;
using UnityEditor;
using UnityEngine;

namespace UniSQLite
{
    public class SQLiteDatabase : ISQLiteDatabase
    {
        private readonly string name;
        private readonly string path;

        private SQLiteDatabaseAsset _asset;
        private SQLiteDatabaseAsset asset => _asset ? _asset : _asset = CreateAsset();

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

        ~SQLiteDatabase()
        {
            UnityMainThreadDispatcher.Instance.Enqueue(() =>
                Debug.LogWarning(AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset))));
        }

        public void ShowTable<T>(T model)
        {
            SQLiteTableAsset tableAssetScriptableObject = ScriptableObject.CreateInstance<SQLiteTableAsset>();
            tableAssetScriptableObject.name = typeof(T).Name;
            tableAssetScriptableObject.Initialize(model);

            AssetDatabase.AddObjectToAsset(tableAssetScriptableObject, CreateAsset());
            AssetDatabase.SaveAssets();
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

        private SQLiteDatabaseAsset CreateAsset()
        {
            SQLiteDatabaseAsset asset = ScriptableObject.CreateInstance<SQLiteDatabaseAsset>();

            AssetDatabase.CreateAsset(asset, $"Assets/{name}.asset");

            return asset;
        }
    }
}