using System;
using System.Data;
using SW.NetCore.DBUtility.Interface;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

namespace SW.NetCore.DBUtility.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected IDbConnection _connection = null;
        protected IDbTransaction _transaction = null;
        protected bool _disposed = false;

        public UnitOfWork(IContext context)
        {
            _connection = context.GetConnect();
            _connection.Open();
        }

        public UnitOfWork(IContext context, string db)
        {
            _connection = context.GetConnect(db);
            _connection.Open();
        }


        public IDbConnection Connection
        {
            get { return _connection; }
        }
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }

            _transaction = null;
            _connection = null;
            _disposed = true;
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }

    }
}
