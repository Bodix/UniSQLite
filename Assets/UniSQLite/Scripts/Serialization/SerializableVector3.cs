using System;
using UnityEngine;

namespace UniSQLite.Scripts.Serialization
{
    [Serializable]
    public class SerializableVector3 : ISerializable
    {
        public float _x, _y, _z;

        public SerializableVector3() { }

        public SerializableVector3(Vector3 vector) : this(vector.x, vector.y, vector.z) { }

        public SerializableVector3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}