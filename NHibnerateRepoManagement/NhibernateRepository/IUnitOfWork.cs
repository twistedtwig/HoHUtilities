using System;

namespace NhibernateRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;

        void RollBack();
    }   
}
