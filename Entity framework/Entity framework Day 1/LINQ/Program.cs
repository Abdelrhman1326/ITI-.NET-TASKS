using System;
using System.Collections.Generic;
using System.Linq;

namespace project
{
    internal class Project
    {
        public static void Main(string[] args)
        {
            #region
            // list
            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };

            // Query 1: Display numbers without any repeated Data and sorted
            var uniqueSortedNumbers = numbers.Distinct().OrderBy(n => n);
            string result0 = string.Join(", ", uniqueSortedNumbers);
            Console.WriteLine(result0 + '\n');

            // Query2: using Query1 result and show each number and it’s multiplication
            Dictionary<int, long> PowOfTwo = new Dictionary<int, long>();
            foreach (int number in uniqueSortedNumbers)
            {
                PowOfTwo[number] = (long)((long)number * (long)number);
            }
            foreach (var pair in PowOfTwo)
            {
                Console.WriteLine($"( Number = {pair.Key}, Multiply = {pair.Value} )");
            }
            Console.WriteLine();
            #endregion
            //-------------------------------------------------------------------------
            #region
            // array:
            string[] names = ["Tom", "Dick", "Harry", "MARY", "Jay"];

            // Query1: Select names with length equal 3.
            var namesOfLengthThree = names.Where(name => name.Length == 3);
            string result1 = string.Join(", ", namesOfLengthThree);
            Console.WriteLine(result1 + '\n');

            // Query2: Select names that contains “a” letter (Capital or Small) then sort them
            // by length (Use toLower method and Contains method)
            var namesStartingWithA = names.Where(name => name.ToLower().Contains('a'));
            string result2 = string.Join(", ", namesStartingWithA);
            Console.WriteLine(result2 + '\n');

            // Query3: Display the first 2 names
            var firstTwoNames = names.Take(2);
            string result3 = string.Join(", ", firstTwoNames);
            Console.WriteLine(result3 + '\n');

            #endregion
            //-------------------------------------------------------------------------
            #region
            // list
            List<Student> students = new List<Student>
            {
                new Student
                {
                    ID = 1,
                    FirstName = "Ali",
                    LastName = "Mohammed",
                    Subjects = new Subject[]
                    {
                        new Subject { Code = 22, Name = "EF" },
                        new Subject { Code = 33, Name = "UML" }
                    }
                },
                new Student
                {
                    ID = 2,
                    FirstName = "Mona",
                    LastName = "Gala",
                    Subjects = new Subject[]
                    {
                        new Subject { Code = 22, Name = "EF" },
                        new Subject { Code = 34, Name = "XML" },
                        new Subject { Code = 25, Name = "JS" }
                    }
                },
                new Student
                {
                    ID = 3,
                    FirstName = "Yara",
                    LastName = "Yousf",
                    Subjects = new Subject[]
                    {
                        new Subject { Code = 22, Name = "EF" },
                        new Subject { Code = 25, Name = "JS" }
                    }
                },
                new Student
                {
                    ID = 1,
                    FirstName = "Ali",
                    LastName = "Ali",
                    Subjects = new Subject[]
                    {
                        new Subject { Code = 33, Name = "UML" }
                    }
                }
            };

            // Query1: Display Full name and number of subjects for each student
            var studentInfo = students.Select(student => new
            {
                FullName = $"{student.FirstName} {student.LastName}",
                SubjectCount = student.Subjects?.Length ?? 0
            });

            foreach (var info in studentInfo)
            {
                Console.WriteLine($"( FullName = {info.FullName}, NumberOfSubjects = {info.SubjectCount} )");
            }
            Console.WriteLine();

            // Query2: Write a query which orders the elements in the list by FirstName
            // Descending then by LastName Ascending and result of query displays only first
            // names and last names for the elements in list
            var sortedStudents = students
                .OrderByDescending(student => student.FirstName)
                .ThenBy(student => student.LastName)
                .Select(student => new
                {
                    student.FirstName,
                    student.LastName
                });

            foreach (var student in sortedStudents)
            {
                Console.WriteLine(student.FirstName + ' ' + student.LastName);
            }
            Console.WriteLine();

            // Query3: Display each student and student’s subject as follow (use selectMany)
            var studentsAndSubjects = students.SelectMany(student =>
            student.Subjects,
            (student, subject) => new
            {
                StudentName = student.FirstName,
                SubjectName = subject.Name
            });

            foreach (var item in studentsAndSubjects)
            {
                Console.WriteLine($"Student: {item.StudentName}, Subject: {item.SubjectName}");
            }
            Console.WriteLine();

            // BONUS: Group students by ID and display their full names
            var studentsByIdGrouped = students.GroupBy(s => s.ID);

            Console.WriteLine("Students grouped by ID:");
            foreach (var group in studentsByIdGrouped)
            {
                Console.WriteLine($"ID {group.Key}:");
                foreach (var student in group)
                {
                    Console.WriteLine($"  - {student.FirstName} {student.LastName}");
                }
            }
            Console.WriteLine();

            //-------------------------------------------------------------------------

            // Query4: Find students with ID equal to 1 and FirstName equal to “Ali”
            var studentsByIdAndName = students
                .Where(student => student.ID == 1 && student.FirstName == "Ali")
                .Select(student => student.FirstName + " " + student.LastName);

            Console.WriteLine("Students with ID 1 and First Name 'Ali':");
            foreach (var name in studentsByIdAndName)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            // Query5: Find the student with the highest number of subjects
            var studentWithMostSubjects = students
                .OrderByDescending(s => s.Subjects?.Length ?? 0)
                .Select(s => $"{s.FirstName} {s.LastName}")
                .FirstOrDefault();

            Console.WriteLine($"Student with the most subjects: {studentWithMostSubjects}");
            Console.WriteLine();

            #endregion
        }

        private class Subject
        {
            public int? Code { get; set; }
            public string? Name { get; set; }
        }

        private class Student
        {
            public int? ID { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public Subject[]? Subjects { get; set; }
        }
    }
}