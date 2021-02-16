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
            
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode && playmodeOnly) 
                    DeleteAsset();
            };
        }

        public bool DeleteAsset()
        {
            return AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
        }

        public void Close()
        {
            string _name = name;
            
            if (DeleteAsset())
                Debug.Log($"Database \"{_name}\" successfully closed");
        }
    }
}