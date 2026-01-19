using LibrarySystem.Application.DTOs.Users;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Domain;

namespace LibrarySystem.Application.Services
{
	/// <summary>
	/// Implements user management operations for the library system.
	/// </summary>
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserService"/> class.
		/// </summary>
		/// <param name="userRepository">The repository for user data access.</param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <paramref name="userRepository"/> is null.
		/// </exception>
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		/// <inheritdoc />
		public async Task<UserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var user = new User(request.Name);
			var userId = await _userRepository.CreateAsync(user, cancellationToken);

			return MapToResponse(user);
		}

		/// <inheritdoc />
		public async Task<UserResponse?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
		{
			var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

			if (user == null)
			{
				return null;
			}

			return MapToResponse(user);
		}

		/// <inheritdoc />
		public async Task<bool> UserExistsAsync(int userId, CancellationToken cancellationToken = default)
		{
			return await _userRepository.ExistsAsync(userId, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<UserResponse>> GetUsersByNameAsync(string name, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be null or empty.", nameof(name));
			}

			var users = await _userRepository.GetByNameAsync(name, cancellationToken);

			return users.Select(MapToResponse).ToList();
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken = default)
		{
			var users = await _userRepository.GetAllAsync(cancellationToken);

			return users.Select(MapToResponse).ToList();
		}

		/// <summary>
		/// Maps a <see cref="User"/> domain entity to a <see cref="UserResponse"/> DTO.
		/// </summary>
		/// <param name="user">The user domain entity to map.</param>
		/// <returns>A <see cref="UserResponse"/> containing the user's data.</returns>
		private static UserResponse MapToResponse(User user)
		{
			return new UserResponse(user.UserId, user.Name);
		}
	}
}
