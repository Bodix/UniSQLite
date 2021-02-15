using System;
using UnityEngine;

namespace UniSQLite.Scripts.Serialization
{
    public static class SerializationUtility
    {
        public static object TryGetSerializableVersion(object @object)
        {
            if (@object is Vector3 vector)
                return new SerializableVector3(vector);
            else
                return @object;
        }
    }
}