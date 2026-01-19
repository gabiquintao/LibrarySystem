using Microsoft.Data.SqlClient;
using System.Data;

namespace LibrarySystem.Infraestructure.Data
{
    /// <summary>
    /// SQL Server implementation of <see cref="IDbConnectionFactory"/>
    /// </summary>
    public class SqlServerConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Connection string used to create SQL Server connections.
        /// </summary>
        private readonly string _connectionString;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerConnectionFactory"/> class.
		/// </summary>
		/// <param name="connectionString">The SQL Server connection string.</param>
		/// <exception cref="ArgumentException">Thrown if <paramref name="connectionString"/> is null or whitespace.</exception>
		public SqlServerConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));

            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates a new SQL Server database connection.
        /// </summary>
        /// <returns>A new <see cref="SqlConnection"/> instance configured with the provided connection string.</returns>
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
