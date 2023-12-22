using Invoice.RecordModels;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly bool useConnection;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool useConnection)
            : base(options)
        {
            this.useConnection = useConnection;
        }
        public DbSet<StatesRecordModel> GrossNetProduceStates { get; set; }
        public DbSet<LoginRecordModel> LoginDetails { get; set; }
        public DbSet<ClientDetailsRecordModel> ClientIdentity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = useConnection ? "EInvoice" : "EwayBillOfficer";
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
