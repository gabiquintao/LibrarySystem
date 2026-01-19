using System.Data;

namespace LibrarySystem.Infraestructure.Data
{
    /// <summary>
    /// Defines a factory responsible for creating database connections.
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates a new database instance.
        /// </summary>
        /// <returns>A new <see cref="IDbConnection"/> configured for the target database.</returns>
        IDbConnection CreateConnection();
    }
}
