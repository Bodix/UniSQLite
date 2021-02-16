using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UniSQLite.Assets;

namespace UniSQLite.Editor
{
    [CustomEditor(typeof(SQLiteTableAsset))]
    public class SQLiteTableAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SQLiteTableAsset asset = (SQLiteTableAsset) target;
            object table = asset.Table;
            
            foreach (PropertyInfo property in GetProperties(table)) 
                EditorGUILayoutCustom.PropertyInfoField(property, asset.Table);

            if (GUILayout.Button("Insert"))
                asset.Insert();
            if (GUILayout.Button("Delete"))
                asset.Delete();
        }

        private IEnumerable<PropertyInfo> GetProperties(object table)
        {
            return table.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => !x.IsDefined(typeof(HideInInspector)));
        }
    }
}