using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            SQLiteTableAsset asset = (SQLiteTableAsset) target;
            object table = asset.Table;
            
            foreach (PropertyInfo property in GetProperties(table))
            {
                Type type = property.PropertyType;
                if (type == typeof(Vector3))
                    property.SetValue(asset.Table, EditorGUILayout.Vector3Field(property.Name, (Vector3) property.GetValue(asset.Table)));
            }

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