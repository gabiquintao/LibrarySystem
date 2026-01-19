using System.Data;
using Microsoft.Data.SqlClient;

namespace LibrarySystem.Infraestructure.Data
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString;

		public DbConnectionFactory()
		{
			_connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LibrarySystemDB;Trusted_Connection=True;MultipleActiveResultSets=true";
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
