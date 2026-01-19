using LibrarySystem.Application.Persistence;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Infraestructure.Data;
using LibrarySystem.Infraestructure.Repositories;
using System.Data;

namespace LibrarySystem.Infraestructure.Persistence
{
	/// <summary>
	/// Implements unit of work pattern for managing database transactions.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbConnection _connection;
		private IDbTransaction? _transaction;
		private bool _disposed;

		public IUserRepository Users { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork"/> class.
		/// </summary>
		/// <param name="connectionFactory">Factory for creating database connections.</param>
		public UnitOfWork(IDbConnectionFactory connectionFactory)
		{
			if (connectionFactory == null)
				throw new ArgumentNullException(nameof(connectionFactory));

			_connection = connectionFactory.CreateConnection();
			_connection.Open();

			Users = new UserRepository(_connection);
		}

		/// <inheritdoc />
		public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			if (_transaction != null)
				throw new InvalidOperationException("Transaction already started.");

			_transaction = _connection.BeginTransaction();
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task CommitAsync(CancellationToken cancellationToken = default)
		{
			if (_transaction == null)
				throw new InvalidOperationException("No transaction to commit.");

			_transaction.Commit();
			_transaction.Dispose();
			_transaction = null;
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task RollbackAsync(CancellationToken cancellationToken = default)
		{
			if (_transaction == null)
				throw new InvalidOperationException("No transaction to rollback.");

			_transaction.Rollback();
			_transaction.Dispose();
			_transaction = null;
			return Task.CompletedTask;
		}

		/// <summary>
		/// Disposes the unit of work and releases database resources.
		/// </summary>
		public void Dispose()
		{
			if (_disposed)
				return;

			_transaction?.Dispose();
			_connection?.Dispose();
			_disposed = true;
		}
	}
}