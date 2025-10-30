using Microsoft.EntityFrameworkCore;
using YourApp.Models;
namespace TryMVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<FormModel> Students { get; set; }
    }
}
