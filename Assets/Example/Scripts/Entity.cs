using SQLite;
using UnityEngine;

namespace Example.Scripts
{
    public class Entity
    {
        [NotNull, PrimaryKey, Unique]
        public int Id { get; set; }
        public string Name { get; set; }
        public Vector3 Position { get; set; }
    }
}