using System;
using System.Linq;
using System.Linq.Expressions;

namespace HeynsLibrary.Repository.Interface
{
    interface IRepositoryBase
    {
        OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters);
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy) where T : class;
        IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class;
        IQueryable<T> GetList<T>() where T : class;
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;
        OperationStatus Update<T>(T entity, params string[] propsToUpdate) where T : class;
    }
}