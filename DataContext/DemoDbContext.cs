using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityDemo.Identity;
using UniversityDemo.Models;

namespace UniversityDemo.Data
{
    public class DemoDbContext :
    //DbContext
    IdentityDbContext<
    ApplicationUser, ApplicationRole, string,
    ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
    ApplicationRoleClaim, ApplicationUserToken>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
           : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(b =>
            {
                b.HasMany(b => b.Enrollments)
                .WithOne(q => q.Student)
                .HasForeignKey(q => q.StudentID)
                .IsRequired();
            });

            modelBuilder.Entity<Course>(b =>
            {
                b.HasMany(b => b.Enrollments)
                .WithOne(q => q.Course)
                .HasForeignKey(q => q.CourseID)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens");
        }
    }
}