using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public static class DbContextOptionsFactory
    {
        public static Dictionary<string, string> ConnectionStrings { get; set; }    
        public static void SetConnectionString(Dictionary<string, string> connectionStrings)
        {
            ConnectionStrings = connectionStrings;
        }

        public static ApplicationDbContext Create(string connectionId)
        {
            if(!string.IsNullOrEmpty(connectionId))
            {
                var connstr = ConnectionStrings[connectionId];
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(connstr);
                return new ApplicationDbContext(optionsBuilder.Options);
            }
            else
            {
                throw new ArgumentNullException(connectionId);
            }
        }
        /*public DbContextOptions<T> CreateOptions<T>(string connectionString) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(connectionString);
            return builder.Options;
        }*/
    }
}
