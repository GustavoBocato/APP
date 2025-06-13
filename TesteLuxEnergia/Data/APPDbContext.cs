using Microsoft.EntityFrameworkCore;
using APP.Models;

namespace APP.Data
{
    public class APPDbContext : DbContext
    {
        public APPDbContext(DbContextOptions<APPDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
