using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WebAPITransport
{
    public class ApplicationDB: DbContext
    {
        public ApplicationDB(DbContextOptions options) : base(options)
        {
        }
        public DbSet<model.Unit> Units { get; set; }
        public DbSet<model.Driver> Drivers { get; set; }
        public DbSet<model.Customer> Customers { get; set; }
    }
}
