using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Data
{
    public class LoginDbContext : IdentityDbContext<User>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {

        }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Aadhar> aadharsCards { get; set; }
        public DbSet<Voter> voterCards { get; set; }
        public DbSet<SmartCard> smartCards { get; set; }
        public DbSet<SmartCardMembers> SmartCardMembers { get; set; }
        public DbSet<PanCard> panCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Billing>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
