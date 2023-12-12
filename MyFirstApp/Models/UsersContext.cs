using Microsoft.EntityFrameworkCore;

namespace MyFirstApp.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { Database.EnsureCreated(); }
    }
}
