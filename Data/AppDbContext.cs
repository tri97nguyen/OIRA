using Microsoft.EntityFrameworkCore;
using parser.Models;

namespace parser.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Rubric> Rubrics { get; set; }
    }
}