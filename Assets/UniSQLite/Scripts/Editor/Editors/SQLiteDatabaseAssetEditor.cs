using UniSQLite.Assets;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Editor.Editors
{
    [CustomEditor(typeof(SQLiteDatabaseAsset))]
    public class SQLiteDatabaseAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SQLiteDatabaseAsset asset = (SQLiteDatabaseAsset) target;
            
            if (GUILayout.Button("Close"))
                asset.DeleteAsset();
        }
    }
}