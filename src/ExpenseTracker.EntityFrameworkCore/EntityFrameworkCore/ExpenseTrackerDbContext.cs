using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ExpenseTracker.Authorization.Roles;
using ExpenseTracker.Authorization.Users;
using ExpenseTracker.MultiTenancy;

namespace ExpenseTracker.EntityFrameworkCore
{
    public class ExpenseTrackerDbContext : AbpZeroDbContext<Tenant, Role, User, ExpenseTrackerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }
    }
}
