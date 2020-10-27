using Microsoft.EntityFrameworkCore;
using parser.Models;

namespace parser.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<RubricCriteria> RubricCriteria { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
    }
}