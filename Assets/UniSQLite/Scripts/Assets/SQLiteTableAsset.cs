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
            Debug.Log("insert");
        }

        public void Delete()
        {
            Debug.Log(AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this)));
        }
    }
}

