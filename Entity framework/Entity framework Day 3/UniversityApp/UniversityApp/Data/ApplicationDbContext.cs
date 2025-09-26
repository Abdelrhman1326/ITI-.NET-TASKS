using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models; // Essential to recognize Instructor, Student, etc.

namespace UniversityApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // DbSet properties are correctly defined for all four entities
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        // connection cofiguration: This is correct for SQL Server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=ABDELRHMAN-LAPT;Database=UniversityAppDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
            );
        }

        // REQUIRED: Override OnModelCreating to configure complex relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- 1. Many-to-Many Configuration (Student <-> Course via StudentCourse) ---

            // Define the COMPOSITE PRIMARY KEY for the junction table (StudentCourse)
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            // Configure the link from StudentCourse to Student
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            // Configure the link from StudentCourse to Course
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            // --- 2. One-to-Many Configuration (Instructor -> Course) ---

            // Configure the link from Course to Instructor (the Explainer)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Explainer)       // A Course has ONE Explainer
                .WithMany(i => i.TaughtCourses) // An Instructor teaches MANY Courses
                .HasForeignKey(c => c.InstructorId)
                .IsRequired(); // Ensures every course must have an assigned instructor

            base.OnModelCreating(modelBuilder);
        }
    }
}