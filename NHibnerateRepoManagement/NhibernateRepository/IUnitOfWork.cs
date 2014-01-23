using System;
using System.Collections.Generic;

namespace NhibernateRepository
{
    public interface IUnitOfWork : IDisposable
    {
        T Get<T>(int id);
        IList<T> Get<T>(Func<T, bool> exp);
        T First<T>(Func<T, bool> exp);

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;

        void RollBack();
    }   
}
