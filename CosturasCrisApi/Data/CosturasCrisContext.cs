using CosturasCrisApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Linq;

namespace CosturasCrisApi.Data
{
    public class CosturasCrisContext : DbContext
    {
        public CosturasCrisContext(DbContextOptions<CosturasCrisContext>dbContextOptions):
            base(dbContextOptions) 
        { 
        }

        public DbSet<Associate> Associate { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ProductService> ProductService { get; set; }
        public DbSet<ServiceCustomer> ServiceCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Associate>(entity =>
            {
                entity.HasIndex(i => new { i.Id }).IsUnique(true);
            });
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasIndex(i => new { i.Id }).IsUnique(true);
            });
            modelBuilder.Entity<ProductService>(entity =>
            {
                entity.HasIndex(i => new { i.Id }).IsUnique(true);
            });
            modelBuilder.Entity<ServiceCustomer>(entity =>
            {
                entity.Property(p => p.RegistrationDate).ValueGeneratedOnAdd().HasValueGenerator<CreatedAtTimeGenerator>();
                entity.HasIndex(i => new { i.Id }).IsUnique(true);
                entity.HasIndex(i => new { i.CustomerId });
                entity.HasIndex(i => new { i.ProductServiceId });
            });
        }

        public class CreatedAtTimeGenerator : ValueGenerator<DateTime>
        {
            public override DateTime Next(EntityEntry entry)
            {
                if (entry == null)
                {
                    throw new ArgumentNullException(nameof(entry));
                }

                return DateTime.Today;
            }
            public override bool GeneratesTemporaryValues { get; }
        }

    }
}
