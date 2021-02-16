using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UniSQLite.Editor
{
    public static class EditorGUILayoutCustom
    {
        public static object NonSerializedPropertyField(PropertyInfo property, object value, params GUILayoutOption[] options)
        {
            Type type = property.PropertyType;

            if (type == typeof(bool))
                return EditorGUILayout.Toggle(property.Name, (bool) value, options);
            else if (type == typeof(Bounds))
                return EditorGUILayout.BoundsField(property.Name, (Bounds) value, options);
            else if (type == typeof(BoundsInt))
                return EditorGUILayout.BoundsIntField(property.Name, (BoundsInt) value, options);
            else if (type == typeof(char))
            {
                string newValue = EditorGUILayout.TextField(property.Name, new string(new[] { (char) value }), options);

                return newValue.Length > 0 ? newValue[0] : default;
            }
            else if (type == typeof(Color))
                return EditorGUILayout.ColorField(property.Name, (Color) value, options);
            else if (type == typeof(AnimationCurve))
                return EditorGUILayout.CurveField(property.Name, (AnimationCurve) value, options);
            else if (type == typeof(double))
                return EditorGUILayout.DoubleField(property.Name, (double) value, options);
            else if (type == typeof(Enum))
                return EditorGUILayout.EnumFlagsField(property.Name, (Enum) value, options);
            else if (type == typeof(float))
                return EditorGUILayout.FloatField(property.Name, (float) value, options);
            else if (type == typeof(Gradient))
                return EditorGUILayout.GradientField(property.Name, (Gradient) value, options);
            else if (type == typeof(int))
                return EditorGUILayout.IntField(property.Name, (int) value, options);
            else if (type == typeof(long))
                return EditorGUILayout.LongField(property.Name, (long) value, options);
            else if (type == typeof(Quaternion))
            {
                Quaternion quaternion = (Quaternion) value;
                Vector4 vector = new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
                
                return EditorGUILayout.Vector4Field(property.Name, vector, options);
            }
            else if (type == typeof(Rect))
                return EditorGUILayout.RectField(property.Name, (Rect) value, options);
            else if (type == typeof(RectInt))
                return EditorGUILayout.RectIntField(property.Name, (RectInt) value, options);
            else if (type == typeof(string))
                return EditorGUILayout.TextField(property.Name, (string) value, options);
            else if (type == typeof(Vector2))
                return EditorGUILayout.Vector2Field(property.Name, (Vector2) value, options);
            else if (type == typeof(Vector2Int))
                return EditorGUILayout.Vector2IntField(property.Name, (Vector2Int) value, options);
            else if (type == typeof(Vector3))
                return EditorGUILayout.Vector3Field(property.Name, (Vector3) value, options);
            else if (type == typeof(Vector3Int))
                return EditorGUILayout.Vector3IntField(property.Name, (Vector3Int) value, options);
            else if (type == typeof(Vector4))
                return EditorGUILayout.Vector4Field(property.Name, (Vector4) value, options);
            else if (type.IsAssignableFrom(typeof(Object)))
                return EditorGUILayout.ObjectField(property.Name, (Object) value, type, false, options);
            else
                throw new NotImplementedException("Drawing field with this type of property is not implemented.");
        }

        public static void PropertyInfoField(PropertyInfo property, object reflectedObj, params GUILayoutOption[] options)
        {
            property.SetValue(reflectedObj, NonSerializedPropertyField(property, property.GetValue(reflectedObj), options));
        }
    }
}