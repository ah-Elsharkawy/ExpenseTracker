using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ExpenseTracker.Authorization.Roles;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.MultiTenancy;
using ExpenseTracker.Models;

namespace ExpenseTracker.EntityFrameworkCore
{
    public class ExpenseTrackerDbContext : AbpZeroDbContext<Tenant, Role, User, ExpenseTrackerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Recurrence> Recurrences { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCategory>()
            .HasKey(uc => new { uc.UserId, uc.CategoryId });
        }
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }
    }
}
