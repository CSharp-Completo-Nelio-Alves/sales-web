using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Models;

namespace SalesWeb.MVC.Data
{
    public class SalesWebContext : DbContext
    {
        public SalesWebContext (DbContextOptions<SalesWebContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = default!;
    }
}
