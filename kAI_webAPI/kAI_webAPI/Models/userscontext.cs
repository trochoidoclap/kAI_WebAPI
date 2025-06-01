using Microsoft.EntityFrameworkCore;
namespace kAI_webAPI.Models
{
    // Fix: Ensure Userscontext inherits from DbContext instead of Users
    public class Userscontext : DbContext
    {
        public Userscontext(DbContextOptions<Userscontext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } = null!;
    }
}
