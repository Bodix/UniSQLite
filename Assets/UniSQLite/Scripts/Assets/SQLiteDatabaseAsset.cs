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

        public bool DeleteAsset()
        {
            string path = AssetDatabase.GetAssetPath(this);

            DestroyImmediate(this, true);

            return path == string.Empty || AssetDatabase.DeleteAsset(path);
        }
    }
}