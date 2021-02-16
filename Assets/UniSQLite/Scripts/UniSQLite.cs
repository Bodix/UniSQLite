using System;
using System.Collections.Generic;
using System.Linq;
using UniSQLite.Dispatcher;
using UniSQLite.Mappers;
using UnityEditor;

namespace UniSQLite
{
    // TO DO:
    // Make support for built-in types.
    
    [InitializeOnLoad]
    public static class UniSQLite
    {
        private static Dictionary<Type, Mapper> _mappers;

        static UniSQLite()
        {
            UnityMainThreadDispatcher.Logs = false;
        }

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
                mappersDictionary.Add(mapperType.BaseType.GenericTypeArguments[0], (Mapper)Activator.CreateInstance(mapperType));

            return mappersDictionary;
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