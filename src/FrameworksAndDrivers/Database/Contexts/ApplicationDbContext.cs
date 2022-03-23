using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FrameworksAndDrivers.Database.Models;
using FrameworksAndDrivers.Database.Attributes;

namespace FrameworksAndDrivers.Database.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(
            builder => builder.AddConsole()
        );
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging();*/
        }

        public virtual DbSet<PaymentModel> Payments { get; set; }
        public virtual DbSet<PaymentStatusModel> PaymentStatuses { get; set; }
        public virtual DbSet<CreditCardModel> CreditCards { get; set; }
        public virtual DbSet<MerchantModel> Merchants { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentModel>(entity =>
            {
                entity.HasIndex(p => new { p.StatusId });
            });

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())  
            //{  
            //    if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)  
            //    {  
            //        modelBuilder
            //            .Entity(entityType.Name)
            //            .Property<DateTime>("CreatedAt")
            //            .HasColumnName("created");  
            //        modelBuilder
            //            .Entity(entityType.Name)
            //            .Property<DateTime>("UpdatedAt")
            //            .HasColumnName("updated");
            //    }  
            //}
            
            base.OnModelCreating(modelBuilder);  
            modelBuilder.Seed();
        }

        //public override int SaveChanges()  
        //{  
        //    ChangeTracker.DetectChanges();  
        //    var timestamp = DateTime.Now;  
  
        //    foreach (var entry in ChangeTracker.Entries()  
        //            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))  
        //    {               
        //        if (entry.Entity.GetType().GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)  
        //        {  
        //            entry.Property("UpdatedAt").CurrentValue = timestamp;  
  
        //            if (entry.State == EntityState.Added)  
        //            {  
        //                entry.Property("CreatedAt").CurrentValue = timestamp; 
        //                entry.Property("UpdatedAt").CurrentValue = timestamp;  
        //            }  
        //        }  
        //    }  
        //    return base.SaveChanges();  
        //}  
    }
}
