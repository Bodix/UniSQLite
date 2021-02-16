using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteTableAsset : ScriptableObject
    {
        [SerializeReference]
        public object[] Rows;

        private SQLiteDatabase database;

        public void Initialize<T>(IEnumerable<T> rows, SQLiteDatabase database)
        {
            Rows = rows.Select(x => (object)x).ToArray();

            this.database = database;
        }

        public void Insert()
        {
            database.InsertAll(Rows);
        }
    }
}