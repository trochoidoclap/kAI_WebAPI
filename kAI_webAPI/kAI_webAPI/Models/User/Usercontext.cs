using Microsoft.EntityFrameworkCore;
namespace kAI_webAPI.Models.User
{
    public class Usercontext(DbContextOptions<Usercontext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
