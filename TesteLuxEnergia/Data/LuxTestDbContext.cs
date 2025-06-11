using Microsoft.EntityFrameworkCore;
using TesteLuxEnergia.Models;

namespace TesteLuxEnergia.Data
{
    public class LuxTestDbContext : DbContext
    {
        public LuxTestDbContext(DbContextOptions<LuxTestDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
