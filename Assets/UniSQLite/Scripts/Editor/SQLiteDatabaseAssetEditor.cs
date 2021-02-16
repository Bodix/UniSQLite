using UnityEditor;
using UnityEngine;
using UniSQLite.Assets;

namespace UniSQLite.Editor
{
    [CustomEditor(typeof(SQLiteDatabaseAsset))]
    public class SQLiteDatabaseAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SQLiteDatabaseAsset asset = (SQLiteDatabaseAsset) target;

            if (GUILayout.Button("Close"))
                asset.Close();
        }
    }
}