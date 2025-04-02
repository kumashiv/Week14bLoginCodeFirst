using Microsoft.EntityFrameworkCore;
using Week14bLoginCodeFirst.Models;

namespace Week14bLoginCodeFirst.AppDbContext
{
    public class DataContext : DbContext      // Inheriting from Entity Framework Core
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }      // Table Name
    }
}
