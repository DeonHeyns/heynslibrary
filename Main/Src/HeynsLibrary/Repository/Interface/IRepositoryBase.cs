using System;
namespace HeynsLibrary.Repository.Interface
{
    interface IRepositoryBase
    {
        OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters);
        T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class;
        System.Linq.IQueryable<T> GetList<T, TKey>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TKey>> orderBy) where T : class;
        System.Linq.IQueryable<T> GetList<T, TKey>(System.Linq.Expressions.Expression<Func<T, TKey>> orderBy) where T : class;
        System.Linq.IQueryable<T> GetList<T>() where T : class;
        System.Linq.IQueryable<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class;
        OperationStatus Update<T>(T entity, params string[] propsToUpdate) where T : class;
    }
}
