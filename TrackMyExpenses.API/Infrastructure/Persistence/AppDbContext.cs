using Microsoft.EntityFrameworkCore;

namespace TrackMyExpenses.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        // public DbSet<Expense> Expenses { get; set; }  
    }
}