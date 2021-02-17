using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UniSQLite.Extensions
{
    public static class CopyExtensions
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
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                
                Array array = obj as Array;
                Array arrayCopy = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++) 
                    arrayCopy.SetValue(DeepCopy(array.GetValue(i)), i);

                return Convert.ChangeType(arrayCopy, type);
            }
            else if (type.IsClass)
            {
                object copy = Activator.CreateInstance(type);
                
                FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (FieldInfo field in fields)
                {
                    object value = field.GetValue(obj);
                    
                    if (value == null)
                        continue;
                    
                    field.SetValue(copy, DeepCopy(value));
                }

                return copy;
            }
            else
                throw new NotImplementedException("Copying an object with this type is not implemented");
        }
    }
}