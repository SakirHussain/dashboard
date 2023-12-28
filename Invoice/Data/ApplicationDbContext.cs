﻿using Invoice.Record_Models;
using Invoice.RecordModels;
using Invoice.Web_Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly bool useConnection;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options/*, bool useConnection*/)
            : base(options)
        {
            //this.useConnection = useConnection;
        }
        public DbSet<StatesRecordModel> GrossNetProduceStates { get; set; }
        public DbSet<LoginRecordModel> LoginDetails { get; set; }
        public DbSet<ClientDetailsRecordModel> ClientIdentity { get; set; }
        public DbSet<ActualTaxCollectionRecordModel> actual_tax_collection { get; set; }
        public DbSet<InvoiceResponseModel> Test{ get; set; }
        public DbSet<ForIdOne> ForIdOne { get; set; }
        public DbSet<ForIdTwo> ForIdTwo { get; set; }
        public DbSet<ForIdThree> ForIdThree { get; set; }
        public DbSet<ForIdFour> ForIdFour { get; set; }
        public DbSet<ForIdFive> ForIdFive { get; set; }
        public DbSet<ForIdSix> ForIdSix { get; set; }


        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = useConnection ? "EInvoice" : "EwayBillOfficer";
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
