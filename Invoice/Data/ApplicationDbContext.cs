using Invoice.Record_Models;
using Invoice.RecordModels;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for StatesRecordModel.
        /// </summary>
        public DbSet<StatesRecordModel> GrossNetProduceStates { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for LoginRecordModel.
        /// </summary>
        public DbSet<LoginRecordModel> LoginDetails { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ClientDetailsRecordModel.
        /// </summary>
        public DbSet<ClientDetailsRecordModel> ClientIdentity { get; set; } 

        /// <summary>
        /// Gets or sets the DbSet for ForIdOne.
        /// </summary>
        public DbSet<ForIdOne> ForIdOne { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ForIdTwo.
        /// </summary>
        public DbSet<ForIdTwo> ForIdTwo { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ForIdThree.
        /// </summary>
        public DbSet<ForIdThree> ForIdThree { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ForIdFour.
        /// </summary>
        public DbSet<ForIdFour> ForIdFour { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ForIdFive.
        /// </summary>
        public DbSet<ForIdFive> ForIdFive { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for ForIdSix.
        /// </summary>
        public DbSet<ForIdSix> ForIdSix { get; set; }
    }
}
