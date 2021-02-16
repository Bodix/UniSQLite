using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UniSQLite.Assets
{
    public class SQLiteTableAsset : ScriptableObject
    {
        [SerializeReference]
        public object[] Rows;

        private object[] initialRows;

        private SQLiteDatabase database;

        public void Initialize<T>(IEnumerable<T> rows, SQLiteDatabase database)
        {
            Rows = rows.Select(x => (object) x).ToArray();
            initialRows = Rows.DeepCopy().ToArray();

            this.database = database;
        }

        public void Update()
        {
            database.UpdateAll(Rows);
        }

        public void Revert()
        {
            Rows = initialRows;
            initialRows = Rows.DeepCopy().ToArray();
        }
    }

    static class Extensions
    {
        public static IEnumerable<T> DeepCopy<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select(x => (T)DeepCopy(x));
        }

        public static object DeepCopy(object obj)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();

            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(
                    type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopy(array.GetValue(i)), i);
                }

                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());
                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                                    BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue == null)
                        continue;
                    field.SetValue(toret, DeepCopy(fieldValue));
                }

                return toret;
            }
            else
                throw new ArgumentException("Unknown type");
        }
    }
}