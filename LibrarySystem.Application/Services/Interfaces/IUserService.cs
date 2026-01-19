using LibrarySystem.Application.DTOs.Users;

namespace LibrarySystem.Application.Services.Interfaces
{
	/// <summary>
	/// Defines operations for managing users in the library system.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Creates a new user in the system.
		/// </summary>
		/// <param name="request">The user creation request containing the user's information.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>A <see cref="UserResponse"/> containing the created user's information.</returns>
		/// <exception cref="ArgumentException">
		/// Thrown when the request contains invalid data.
		/// </exception>
		Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves a user by their unique identifier.
		/// </summary>
		/// <param name="userId">The unique identifier of the user to retrieve.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>
		/// A <see cref="UserResponse"/> containing the user's information if found; 
		/// otherwise, null if no user exists with the specified ID.
		/// </returns>
		Task<UserResponse?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Checks whether a user with the specified identifier exists in the system.
		/// </summary>
		/// <param name="userId">The unique identifier of the user to check.</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>
		/// True if a user with the specified ID exists; otherwise, false.
		/// </returns>
		Task<bool> UserExistsAsync(int userId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all users whose name matches the specified value.
		/// </summary>
		/// <param name="name">The name to search for (exact match).</param>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>
		/// A read-only collection of <see cref="UserResponse"/> objects representing all users with the specified name.
		/// Returns an empty collection if no matches are found.
		/// </returns>
		Task<IReadOnlyCollection<UserResponse>> GetUsersByNameAsync(string name, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves all users in the system.
		/// </summary>
		/// <param name="cancellationToken">Token to cancel the operation.</param>
		/// <returns>
		/// A read-only collection of <see cref="UserResponse"/> objects representing all users.
		/// Returns an empty collection if no users exist.
		/// </returns>
		Task<IReadOnlyCollection<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default);
	}
}
