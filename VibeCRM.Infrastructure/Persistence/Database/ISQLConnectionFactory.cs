using System.Data;

namespace VibeCRM.Infrastructure.Persistence.Database
{
    /// <summary>
    /// Interface for creating SQL connections to the database
    /// </summary>
    public interface ISQLConnectionFactory
    {
        /// <summary>
        /// Creates a new SQL connection.
        /// </summary>
        /// <returns>An open database connection</returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates a new SQL connection without opening it.
        /// </summary>
        /// <returns>A database connection that is not yet open</returns>
        IDbConnection CreateClosedConnection();
    }
}