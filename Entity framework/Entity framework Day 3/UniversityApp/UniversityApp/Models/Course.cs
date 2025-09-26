// Models/Course.cs
using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Course
    {
        // Primary Key
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Foreign Key: Links to the Instructor.
        public int? InstructorId { get; set; }

        // Navigation Property (One-to-Many): A Course has one Explainer (Instructor).
        public Instructor Explainer { get; set; }

        // Navigation Property (Many-to-Many): Many Students are assigned to this course (via StudentCourse).
        public ICollection<StudentCourse> StudentCourses { get; set; }

        // Constructor to initialize the collection
        public Course()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}