﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace NhibernateRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ISession _session;
        private ITransaction _transaction;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        private ISession Session { get { return _session; } }

        public void Dispose()
        {
            if (_session != null && _transaction != null && _transaction.IsActive)
            {
                _transaction.Commit();

                _session.Close();
                _transaction.Dispose();
                _session.Dispose();
            }            

            _transaction = null;
            _session = null;           
        }

        public void RollBack()
        {
            if (_session != null && _transaction != null && _transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }

        public T Get<T>(int id)
        {
            return _session.Get<T>(id);
        }

        public IList<T> Get<T>(Func<T, bool> exp)
        {
            return _session.Query<T>().Where(exp).ToList();
        }

        public T First<T>(Func<T, bool> exp)
        {
            return _session.Query<T>().Where(exp).FirstOrDefault();
        }

        public void Add<T>(T entity) where T : class 
        {
            Session.SaveOrUpdate(entity);
        }

        public void Update<T>(T entity) where T : class 
        {
            Session.Merge(entity);
        }

        public void Remove<T>(T entity) where T : class 
        {
            Session.Delete(entity);
        }
    }
}
