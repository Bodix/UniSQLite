using Example.Scripts;
using UniSQLite.Scripts;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteTableAsset : ScriptableObject
    {
        [SerializeReference]
        public object Table;
    
        public void Initialize<T>(T table)
        {
            Table = table;
        }

        public void Insert()
        {
            Debug.Log(((Entity)Table).Positions);
        }

        public void Delete()
        {
            Debug.Log(AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this)));
        }
    }
}

