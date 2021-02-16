using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniSQLite.Assets;
using UnityEditor;
using UnityEngine;

namespace UniSQLite.Editor.Editors
{
    [CustomEditor(typeof(SQLiteTableAsset))]
    public class SQLiteTableAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SQLiteTableAsset asset = (SQLiteTableAsset) target;
            object[] rows = asset.Rows;

            foreach (object row in rows)
                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                    foreach (PropertyInfo property in GetProperties(row))
                        EditorGUILayoutCustom.PropertyInfoField(property, row);

            if (GUILayout.Button("Insert"))
                asset.Insert();
        }

        private IEnumerable<PropertyInfo> GetProperties(object table)
        {
            return table.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => !x.IsDefined(typeof(HideInInspector)));
        }
    }
}