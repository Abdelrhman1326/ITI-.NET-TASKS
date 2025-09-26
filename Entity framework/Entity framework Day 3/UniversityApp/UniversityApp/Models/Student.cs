// Models/Student.cs
using System;
using System.Collections.Generic;

namespace UniversityApp.Models
{
    public class Student
    {
        // Primary Key
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Navigation Property: Many-to-Many relationship with Course (via StudentCourse).
        public ICollection<StudentCourse> StudentCourses { get; set; }

        // Constructor to initialize the collection
        public Student()
        {
            StudentCourses = new List<StudentCourse>();
        }
    }
}