using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace VibeCRM.Infrastructure.Persistence.Database
{
    /// <summary>
    /// Factory for creating SQL connections to the database
    /// </summary>
    public class SQLConnectionFactory : ISQLConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLConnectionFactory"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the connection string</param>
        public SQLConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' is missing");
        }

        /// <summary>
        /// Creates a new SQL connection.
        /// </summary>
        /// <returns>An open database connection</returns>
        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Creates a new SQL connection without opening it.
        /// </summary>
        /// <returns>A database connection that is not yet open</returns>
        public IDbConnection CreateClosedConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}