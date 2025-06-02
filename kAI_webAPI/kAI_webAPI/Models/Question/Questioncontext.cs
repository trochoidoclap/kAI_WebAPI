using Microsoft.EntityFrameworkCore;
namespace kAI_webAPI.Models.Question
{
    public class Questioncontext(DbContextOptions<Questioncontext> options) : DbContext(options)
    {
        public DbSet<Question> Questions { get; set; } = null!;
    }
}
