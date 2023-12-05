using Invoice.RecordModels;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<StatesRecordModel> GrossNetProduceStates { get; set; }
        public DbSet<LoginRecordModel> LoginDetails { get; set; }
        public DbSet<ClientDetailsRecordModel> ClientIdentity { get; set; }  

    }
}
