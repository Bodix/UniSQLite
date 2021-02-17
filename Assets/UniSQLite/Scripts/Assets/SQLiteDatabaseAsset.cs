using UnityEditor;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteDatabaseAsset : ScriptableObject
    {
        private bool playmodeOnly;
        
        private void Awake()
        {
            playmodeOnly = Application.isPlaying;
        }

        public void DeleteAssetIfPlaymodeOnly()
        {
            if (playmodeOnly)
                DeleteAsset();
        }

        public void Close()
        {
            string _name = name;
            
            if (DeleteAsset())
                Debug.Log($"Database \"{_name}\" successfully closed");
        }

        private bool DeleteAsset()
        {
            string path = AssetDatabase.GetAssetPath(this);
            
            DestroyImmediate(this, true);
            
            return AssetDatabase.DeleteAsset(path);
        }
    }
}