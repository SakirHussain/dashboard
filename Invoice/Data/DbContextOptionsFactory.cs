using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    /// <summary>
    /// Factory class for creating DbContextOptions for ApplicationDbContext.
    /// </summary>
    public static class DbContextOptionsFactory
    {
        /// <summary>
        /// Gets or sets the connection strings used for creating DbContextOptions.
        /// </summary>
        public static Dictionary<string, string> ConnectionStrings { get; set; }

        /// <summary>
        /// Sets the connection strings to be used for creating DbContextOptions.
        /// </summary>
        /// <param name="connectionStrings">A dictionary containing connection strings.</param>
        public static void SetConnectionString(Dictionary<string, string> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }

        /// <summary>
        /// Creates an instance of ApplicationDbContext with the specified connectionId.
        /// </summary>
        /// <param name="connectionId">The identifier for the desired connection string.</param>
        /// <returns>An instance of ApplicationDbContext with the specified connection string.</returns>
        public static ApplicationDbContext Create(string connectionId)
        {
            if (!string.IsNullOrEmpty(connectionId))
            {
                // Retrieve the connection string based on the provided connectionId
                var connectionString = ConnectionStrings[connectionId];

                // Configure DbContextOptionsBuilder with the SQL Server connection string
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                // Return a new instance of ApplicationDbContext with the configured options
                return new ApplicationDbContext(optionsBuilder.Options);
            }
            else
            {
                // Throw an exception if connectionId is null or empty
                throw new ArgumentNullException(nameof(connectionId), "ConnectionId cannot be null or empty.");
            }
        }
    }
}
