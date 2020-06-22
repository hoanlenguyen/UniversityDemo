using Microsoft.EntityFrameworkCore;
using UniversityDemo.Models;

namespace UniversityDemo.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");

            modelBuilder.Entity<Enrollment>()
                .HasOne(q => q.Student)
                .WithMany(p => p.Enrollments)
                .HasForeignKey(e => e.StudentID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
               .HasOne(q => q.Course)
               .WithMany(p => p.Enrollments)
               .HasForeignKey(e => e.CourseID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}