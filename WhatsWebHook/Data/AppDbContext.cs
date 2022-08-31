using Microsoft.EntityFrameworkCore;
using WhatsWebHook.Models;

namespace WhatsWebHook.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Users> user { get; set; }
        public DbSet<Messages> messages { get; set; }
        public DbSet<UserComments> UserComments { get; set; }
        
    }
}
