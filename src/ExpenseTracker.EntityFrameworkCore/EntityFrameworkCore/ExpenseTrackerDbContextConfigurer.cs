using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.EntityFrameworkCore
{
    public static class ExpenseTrackerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ExpenseTrackerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ExpenseTrackerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
