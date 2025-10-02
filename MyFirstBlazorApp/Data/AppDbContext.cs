using Microsoft.EntityFrameworkCore;
using MyFirstBlazorApp.Models;

namespace MyFirstBlazorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
