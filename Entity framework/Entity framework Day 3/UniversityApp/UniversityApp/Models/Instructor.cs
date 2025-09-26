// Models/Instructor.cs
using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Instructor
    {
        // Primary Key
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        // Navigation Property: One instructor can explain many courses.
        public ICollection<Course> TaughtCourses { get; set; }

        // Constructor to initialize the collection
        public Instructor()
        {
            TaughtCourses = new List<Course>();
        }
    }
}