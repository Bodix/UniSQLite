using System.Collections.Generic;
using System.Linq;
using UniSQLite.Extensions;
using UnityEngine;

namespace UniSQLite.Assets
{
#if UNITY_EDITOR
    public class SQLiteTableAsset : ScriptableObject
    {
        [SerializeReference]
        public object[] Rows;

        private object[] initialRows;

        private SQLiteDatabase database;

        public void Initialize<T>(IEnumerable<T> rows, SQLiteDatabase database)
        {
            Initialize(rows.Select(x => (object) x).ToArray());

            this.database = database;
        }

        private void Initialize(object[] rows)
        {
            Rows = rows;
            initialRows = Rows.DeepCopy().ToArray();
        }

        public void Update()
        {
            database.UpdateAll(Rows);
        }

        public void Revert()
        {
            Initialize(initialRows);
        }
    }
#endif
}