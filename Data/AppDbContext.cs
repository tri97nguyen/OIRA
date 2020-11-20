using Microsoft.EntityFrameworkCore;
using parser.Models;

namespace parser.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<RubricCriteria> RubricCriteria { get; set; }
        public DbSet<RubricCriteriaElement> RubricCriteriaElements { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
    }
}