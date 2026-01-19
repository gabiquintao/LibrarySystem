using Dapper;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain;
using LibrarySystem.Infraestructure.Data;
using System.Data;

namespace LibrarySystem.Infraestructure.Repositories
{
	/// <summary>
	/// Implements persistence operations for <see cref="User"/> entities.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		private readonly IDbConnection? _sharedConnection;
		private readonly IDbConnectionFactory? _connectionFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class for standalone use.
		/// </summary>
		/// <param name="connectionFactory">Factory for creating database connections.</param>
		public UserRepository(IDbConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class for UnitOfWork use.
		/// </summary>
		/// <param name="connection">Shared database connection from UnitOfWork.</param>
		internal UserRepository(IDbConnection connection)
		{
			_sharedConnection = connection ?? throw new ArgumentNullException(nameof(connection));
		}

		/// <inheritdoc />
		public async Task<int> CreateAsync(User user, CancellationToken cancellationToken = default)
		{
			const string sql = @"
				INSERT INTO Users (Name)
				VALUES (@Name);
				SELECT CAST(SCOPE_IDENTITY() AS INT);";

			var command = new CommandDefinition(
				commandText: sql,
				parameters: new { user.Name },
				cancellationToken: cancellationToken
			);

			int id;
			if (_sharedConnection != null)
			{
				id = await _sharedConnection.ExecuteScalarAsync<int>(command);
			}
			else
			{
				using var connection = _connectionFactory!.CreateConnection();
				id = await connection.ExecuteScalarAsync<int>(command);
			}

			user.SetId(id);
			return id;
		}

		/// <inheritdoc />
		public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken = default)
		{
			const string sql = @"
				SELECT UserId, Name
				FROM Users
				WHERE UserId = @UserId;";

			var command = new CommandDefinition(
				commandText: sql,
				parameters: new { UserId = userId },
				cancellationToken: cancellationToken
			);

			if (_sharedConnection != null)
			{
				return await _sharedConnection.QuerySingleOrDefaultAsync<User>(command);
			}
			else
			{
				using var connection = _connectionFactory!.CreateConnection();
				return await connection.QuerySingleOrDefaultAsync<User>(command);
			}
		}

		/// <inheritdoc />
		public async Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken = default)
		{
			const string sql = @"
				SELECT CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END
				FROM Users
				WHERE UserId = @UserId;";

			var command = new CommandDefinition(
				commandText: sql,
				parameters: new { UserId = userId },
				cancellationToken: cancellationToken
			);

			if (_sharedConnection != null)
			{
				return await _sharedConnection.ExecuteScalarAsync<bool>(command);
			}
			else
			{
				using var connection = _connectionFactory!.CreateConnection();
				return await connection.ExecuteScalarAsync<bool>(command);
			}
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<User>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
		{
			const string sql = @"
				SELECT UserId, Name
				FROM Users
				WHERE Name = @Name;";

			var command = new CommandDefinition(
				commandText: sql,
				parameters: new { Name = name },
				cancellationToken: cancellationToken
			);

			IEnumerable<User> users;
			if (_sharedConnection != null)
			{
				users = await _sharedConnection.QueryAsync<User>(command);
			}
			else
			{
				using var connection = _connectionFactory!.CreateConnection();
				users = await connection.QueryAsync<User>(command);
			}

			return users.ToList();
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			const string sql = @"
				SELECT UserId, Name
				FROM Users;";

			var command = new CommandDefinition(
				commandText: sql,
				cancellationToken: cancellationToken
			);

			IEnumerable<User> users;
			if (_sharedConnection != null)
			{
				users = await _sharedConnection.QueryAsync<User>(command);
			}
			else
			{
				using var connection = _connectionFactory!.CreateConnection();
				users = await connection.QueryAsync<User>(command);
			}

			return users.ToList();
		}
	}
}