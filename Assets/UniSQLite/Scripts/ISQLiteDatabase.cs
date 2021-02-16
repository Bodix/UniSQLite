using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UniSQLite
{
    public interface ISQLiteDatabase
    {
        T[] GetAll<T>(Expression<Func<T, bool>> predicate = null) where T : new();

        T Get<T>(Expression<Func<T, bool>> predicate = null) where T : new();

        T Insert<T>(T data) where T : new();

        T InsertOrReplace<T>(T data) where T : new();

        IEnumerable<T> InsertAll<T>(IEnumerable<T> data) where T : new();

        void DeleteAll<T>() where T : new();
    }
}
