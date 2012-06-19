using System;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

using HeynsLibrary.Repository.Interface;

namespace HeynsLibrary.Repository
{
    public class RepositoryBase<C> : IDisposable, IRepositoryBase
       where C : ObjectContext, new()
    {
        private C _DataContext;

        public virtual C DataContext
        {
            get
            {
                if (_DataContext == null)
                {
                    _DataContext = new C();
                }
                return _DataContext;
            }
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                using (DataContext)
                {
                    return DataContext.CreateObjectSet<T>().Where(predicate).SingleOrDefault();
                }
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }

        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return DataContext.CreateObjectSet<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                //  Todo Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList<T>().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual IQueryable<T> GetList<T>() where T : class
        {
            try
            {
                return DataContext.CreateObjectSet<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual OperationStatus Update<T>(T entity, params string[] propsToUpdate) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                DataContext.CreateObjectSet<T>().Attach(entity);
                var entry = DataContext.ObjectStateManager.GetObjectStateEntry(entity);                
                foreach (var propName in propsToUpdate)
                {
                    entry.SetModifiedProperty(propName);
                }
                opStatus.Status = DataContext.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error updating " + typeof(T) + ".", exp);
            }

            return opStatus;
        }

        public OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            var opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.RecordsAffected = DataContext.ExecuteStoreCommand(cmdText, parameters);
            }
            catch (Exception exp)
            {
                OperationStatus.CreateFromException("Error executing store command: ", exp);
            }
            return opStatus;
        }

        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}