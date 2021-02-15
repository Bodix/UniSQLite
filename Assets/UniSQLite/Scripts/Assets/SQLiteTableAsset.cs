using UniSQLite.Scripts;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteTableAsset : ScriptableObject
    {
        [SerializeReference]
        public object Table;
    
        public void Initialize<T>(T table) where T : DTO
        {
            Table = table;
        }

        public void Insert()
        {
            Debug.Log("Insert");
        }

        public void Delete()
        {
            Debug.Log(AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this)));
        }
    }
}

