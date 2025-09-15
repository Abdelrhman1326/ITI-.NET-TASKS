using System;
using System.Collections.Generic;

namespace Program
{
    internal class Program
    {
        public class Person
        {
            private string _name { get; set; }
            private int _age { get; set; }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public int Age
            {
                get { return _age; }
                set { _age = value; }
            }

            public Person(string name, int age)
            {
                this.Name = name;
                this.Age = age;
            }
        }

        public class Course
        {
            public string Name { get; set; }
            public Instructor Instructor { get; private set; }
            public List<Student> Students { get; } = new List<Student>();

            public void SetInstructor(Instructor instructor)
            {
                Instructor = instructor;
            }
        }

        public class Instructor : Person
        {
            public Instructor(string name, int age) : base(name, age) { }
            public void TeachCourse(Course course)
            {
                course.SetInstructor(this);
            }
        }

        public class Student : Person
        {
            public Student(string name, int age) : base(name, age) { }
            public List<Course> CoursesEnrolled { get; } = new List<Course>();

            public void RegisterCourse(Course course)
            {
                CoursesEnrolled.Add(course);
                course.Students.Add(this);
            }
        }

        public class Department
        {
            public string Name { get; set; }
            public List<Person> Employees { get; } = new List<Person>();
        }

        public class Company
        {
            public string Name { get; set; }
            public List<Department> Departments { get; } = new List<Department>();
        }

        static void Main(string[] args)
        {

        }
    }
}