using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public class DbContextOptionsFactory
    {
        public DbContextOptions<T> CreateOptions<T>(string connectionString) where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlServer(connectionString);
            return builder.Options;
        }
    }
}
