// Models/StudentCourse.cs
using System;

namespace UniversityApp.Models
{
    // EF Core will use this entity to create the junction table.
    public class StudentCourse
    {
        // Composite Foreign/Primary Keys (configured in DbContext's OnModelCreating)
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime RegistrationDate { get; set; } // Extra data for the relationship

        // Navigation Properties to the principal entities
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}