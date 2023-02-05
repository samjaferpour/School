using Microsoft.EntityFrameworkCore;
using School.Entities;

namespace School.Contexts
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
