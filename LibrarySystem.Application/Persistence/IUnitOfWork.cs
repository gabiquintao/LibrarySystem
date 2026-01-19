using LibrarySystem.Application.Repositories;

namespace LibrarySystem.Application.Persistence
{
	/// <summary>
	/// Defines a unit of work for managing transactions across multiple repositories.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Gets the user repository.
		/// </summary>
		IUserRepository Users { get; }

		/// <summary>
		/// Begins a new database transaction.
		/// </summary>
		Task BeginTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Commits the current transaction.
		/// </summary>
		Task CommitAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Rolls back the current transaction.
		/// </summary>
		Task RollbackAsync(CancellationToken cancellationToken = default);
	}
}