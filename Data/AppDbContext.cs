using ManageEmployeApp.Modle;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployeApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
