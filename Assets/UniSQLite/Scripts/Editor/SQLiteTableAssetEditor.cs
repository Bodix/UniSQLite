using UniSQLite.Assets;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Scripts.Editor
{
    [CustomEditor(typeof(SQLiteTableAsset))]
    public class SQLiteTableAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Insert"))
                ((SQLiteTableAsset) target).Insert();
            if (GUILayout.Button("Delete"))
                ((SQLiteTableAsset) target).Delete();
        }
    }
}