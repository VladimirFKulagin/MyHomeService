using Microsoft.EntityFrameworkCore;
using MyHomeService.Models;

namespace MyHomeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
