using System;
using System.Collections.Generic;
using System.Linq;
using UniSQLite.Mappers;

namespace UniSQLite
{
    // TO DO:
    // 1. Make support for property fields with built-in types in asset editor.
    // 2. Make reverting method with re-getting all rows.
    // 3. Make auto-destruction of playmode assets after recompiling too.

    public static class UniSQLite
    {
        private static Dictionary<Type, Mapper> _mappers;

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