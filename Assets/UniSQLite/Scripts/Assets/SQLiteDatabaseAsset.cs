using UnityEditor;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteDatabaseAsset : ScriptableObject
    {
        private void Awake()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                Debug.Log(state);
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    DeleteAsset();
                    // DestroyImmediate(this, true);
                    Debug.Log(this);
                }
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