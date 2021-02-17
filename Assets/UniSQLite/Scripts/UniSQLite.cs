using System;
using System.Collections.Generic;
using System.Linq;
using UniSQLite.Assets;
using UniSQLite.Mappers;
using UnityEditor;
using UnityEngine;

namespace UniSQLite
{
    // TO DO:
    // 1. Make support for property fields with built-in types in asset editor.
    // 2. Make rows creation and deletion inside table asset.
    // 3. Make reverting method with re-getting all rows.

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class UniSQLite
    {
        private static Dictionary<Type, Mapper> _mappers;

#if UNITY_EDITOR
        static UniSQLite()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                SQLiteDatabaseAsset[] assets = Resources.FindObjectsOfTypeAll<SQLiteDatabaseAsset>();
                foreach (SQLiteDatabaseAsset asset in assets)
                {
                    if (state == PlayModeStateChange.ExitingPlayMode)
                        asset.DeleteAssetIfPlaymodeOnly();
                }
            };
        }
#endif

        private static Dictionary<Type, Mapper> mappers =>
            _mappers ?? (_mappers = InitializeMapperTypes());

        private static Dictionary<Type, Mapper> InitializeMapperTypes()
        {
            Type[] mapperTypes = (from type in typeof(UniSQLite).Assembly.GetExportedTypes()
                where type.BaseType != null &&
                      type.BaseType.IsGenericType &&
                      type.BaseType.GetGenericTypeDefinition() == typeof(Mapper<>)
                select type).ToArray();

            Dictionary<Type, Mapper> mappersDictionary = new Dictionary<Type, Mapper>(mapperTypes.Length);
            foreach (Type mapperType in mapperTypes)
                mappersDictionary.Add(mapperType.BaseType.GenericTypeArguments[0], (Mapper) Activator.CreateInstance(mapperType));

            return mappersDictionary;
        }

        public static void AddMapper(Type forType, Mapper mapper)
        {
            mappers.Add(forType, mapper);
        }

        public static bool HasMapperFor(Type type)
        {
            return mappers.ContainsKey(type);
        }

        public static Mapper GetMapperFor(Type type)
        {
            return mappers[type];
        }
    }
}