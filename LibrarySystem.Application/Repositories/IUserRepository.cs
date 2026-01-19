using LibrarySystem.Domain;

namespace LibrarySystem.Application.Repositories
{
	/// <summary>
	/// Defines persistence operations for <see cref="User"/> entities.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Persists a new user in the database.
		/// </summary>
		/// <param name="user">The user to be created.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>The generated user identifier.</returns>
		Task<int> CreateAsync(User user, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves a user by its unique identifier.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>The user if found; otherwise, null.</returns>
		Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Checks whether a user exists with the given identifier.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>True if the user exists; otherwise, false.</returns>
		Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all users with the specified name.
		/// </summary>
		/// <param name="name">The user name.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>A read-only collection of users with the specified name.</returns>
		Task<IReadOnlyCollection<User>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all users.
		/// </summary>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>A read-only collection of users.</returns>
		Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);
	}
}