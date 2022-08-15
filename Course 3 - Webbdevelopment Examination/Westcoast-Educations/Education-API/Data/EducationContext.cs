using Education_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Data
{
  public class EducationContext : IdentityDbContext
    {

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Skill> Skills => Set<Skill>();





        public EducationContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}