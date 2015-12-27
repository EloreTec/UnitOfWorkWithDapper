using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace UnitOfWorkWithDapper.Core
{
    /// <summary>
    /// Represents a context to Dapper.NET
    /// </summary>
    public abstract class DapperContext : IContext
    {
        #region Fields

        /// <summary>
        /// Indicates if transaction is started.
        /// </summary>
        private bool _isTransactionStarted;

        /// <summary>
        /// The connection object to database.
        /// </summary>
        private IDbConnection _connection;

        /// <summary>
        /// The transaction object to connection.
        /// </summary>
        private IDbTransaction _transaction;

        /// <summary>
        /// The command timeout.
        /// </summary>
        private int? _commandTimeout = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Indicates if transaction is started.
        /// </summary>
        public bool IsTransactionStarted
        {
            get
            {
                return _isTransactionStarted;
            }
        }

        #endregion Properties

        #region Abstract methods

        /// <summary>
        /// Creates connection object to database.
        /// </summary>
        /// <returns>The connection object.</returns>
        protected abstract IDbConnection CreateConnection();

        #endregion Abstract methods

        #region Methods

        protected DapperContext()
        {
            _connection = CreateConnection();

            DebugPrint("Connection started.");
        }

        private void DebugPrint(string message)
        {
#if DEBUG
            Debug.Print(">>> UnitOfWorkWithDapper - Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, message);
#endif
        }

        #region Transaction

        /// <summary>
        /// Begins transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (_isTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");

            _transaction = _connection.BeginTransaction();

            _isTransactionStarted = true;

            DebugPrint("Transaction started.");
        }

        /// <summary>
        /// Commits operations of transaction.
        /// </summary>
        public void Commit()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Commit();
            _transaction = null;

            _isTransactionStarted = false;

            DebugPrint("Transaction committed.");
        }

        /// <summary>
        /// Rollbacks operations of transaction.
        /// </summary>
        public void Rollback()
        {
            if (!_isTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;

            _isTransactionStarted = false;

            DebugPrint("Transaction rollbacked and disposed.");
        }

        #endregion Transaction

        #region Execute

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The sql.</param>
        /// <param name="param">The parameters.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return SqlMapper.Execute(_connection, sql, param, _transaction, _commandTimeout, commandType);
        }

        #endregion Execute

        #region ExecuteReader

        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="sql">The sql.</param>
        /// <param name="param">The parameters.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return SqlMapper.ExecuteReader(_connection, sql, param, _transaction, _commandTimeout, commandType);
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The sql.</param>
        /// <param name="param">The parameters.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return SqlMapper.ExecuteScalar<T>(_connection, sql, param, _transaction, _commandTimeout, commandType);
        }

        #endregion ExecuteScalar

        #region Query

        /// <summary>
        /// Executes a query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <param name="sql">The sql.</param>
        /// <param name="param">The parameters.</param>
        /// <param name="commandType">The command type.</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return SqlMapper.Query<T>(_connection, sql, param, _transaction, true, _commandTimeout, commandType);
        }

        /// <summary>
        /// Maps a query to objects.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the record set.</typeparam>
        /// <typeparam name="TSecond">The second type in the record set.</typeparam>
        /// <typeparam name="TReturn">The return type.</typeparam>
        /// <param name="sql">The sql.</param>
        /// <param name="map">The mapper of types.</param>
        /// <param name="param">The parameters.</param>
        /// <param name="splitOn">The Field we should split and read the second object from (default: id).</param>
        /// <param name="commandType">The command type.</param>
        /// <returns></returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
        {
            return SqlMapper.Query<TFirst, TSecond, TReturn>(_connection, sql, map, param, _transaction, true, splitOn, _commandTimeout, commandType);
        }

        #endregion Query

        public void Dispose()
        {
            if (_isTransactionStarted)
                Rollback();

            _connection.Close();
            _connection.Dispose();
            _connection = null;

            DebugPrint("Connection closed and disposed.");
        }

        #endregion Methods
    }
}