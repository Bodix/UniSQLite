using System;
using SQLite;
using UniSQLite.Scripts;
using UnityEngine;

namespace Example.Scripts
{
    // [Serializable]
    public class Entity
    {
        [NotNull, PrimaryKey, Unique]
        // [HideInInspector]
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector3 Positions { get; set; }
    }
}