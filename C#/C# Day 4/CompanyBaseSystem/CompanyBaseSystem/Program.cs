namespace Program
{
    internal class Program
    {
        public class Person
        {
            private string _name;
            private int _age;

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
                this._name = name;
                this._age = age;
            }

            public virtual void Introduce()
            {
                Console.WriteLine($"Hello, i am a Person, my name is {this.Name}.");
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
            public List<Course> CoursesTeaching { get; } = new List<Course>();

            public void TeachCourse(Course course)
            {
                course.SetInstructor(this);
                CoursesTeaching.Add(course);
            }

            public override void Introduce()
            {
                Console.WriteLine($"Hello, i am an Instructor, and my name is {this.Name}.");
            }
        }

        public class Student : Person
        {
            public List<int> Grades = new List<int>();
            public Student(string name, int age) : base(name, age) { }
            public List<Course> CoursesEnrolled { get; } = new List<Course>();

            public void RegisterCourse(Course course)
            {
                CoursesEnrolled.Add(course);
                course.Students.Add(this);
            }

            public override void Introduce()
            {
                Console.WriteLine($"Hello, i am a Student, and my name is {this.Name}.");
            }

            public int TotalMark()
            {
                return Grades.Sum();
            }

            public List<int> getMarks()
            {
                return Grades;
            }

            public static bool operator ==(Student student, int grade)
            {
                if (ReferenceEquals(student, null))
                {
                    return false;
                }
                return student.TotalMark() == grade;
            }

            public static bool operator !=(Student student, int grade)
            {
                if (ReferenceEquals(student, null))
                {
                    return true;
                }
                return !(student == grade);
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
            // Create a company
            var myCompany = new Company { Name = "Tech Inc." };

            // Create departments and add them to the company
            var itDept = new Department { Name = "IT" };
            var hrDept = new Department { Name = "HR" };
            myCompany.Departments.Add(itDept);
            myCompany.Departments.Add(hrDept);

            // Create people (employees) and add them to departments
            var instructor1 = new Instructor("Jane Smith", 35);
            var instructor2 = new Instructor("Mark Davis", 40);
            var student1 = new Student("Bob Johnson", 20);
            var student2 = new Student("Alice Williams", 21);
            itDept.Employees.Add(instructor1);
            itDept.Employees.Add(instructor2);
            itDept.Employees.Add(student1);
            hrDept.Employees.Add(student2);

            // Create courses
            var programmingCourse = new Course { Name = "C# Programming" };
            var marketingCourse = new Course { Name = "Digital Marketing" };
            var designCourse = new Course { Name = "UX/UI Design" };
            var cybersecurityCourse = new Course { Name = "Cybersecurity Fundamentals" };

            // Assign courses to instructors and students
            instructor1.TeachCourse(programmingCourse);
            instructor2.TeachCourse(cybersecurityCourse);
            student1.RegisterCourse(programmingCourse);
            student1.RegisterCourse(designCourse);
            student2.RegisterCourse(marketingCourse);

            // Assign grades to a student
            student1.Grades.Add(10);
            student1.Grades.Add(10);
            student2.Grades.Add(15);
            student2.Grades.Add(15);


            Console.WriteLine($"Company Name: {myCompany.Name}\n");

            foreach (var department in myCompany.Departments)
            {
                Console.WriteLine($"--- Department: {department.Name} ---");
                foreach (var person in department.Employees)
                {
                    person.Introduce();

                    if (person is Student student)
                    {
                        Console.WriteLine($"Courses Enrolled: {string.Join(", ", student.CoursesEnrolled.Select(c => c.Name))}");
                    }
                    else if (person is Instructor instructor)
                    {
                        Console.WriteLine($"Courses Teaching: {string.Join(", ", instructor.CoursesTeaching.Select(c => c.Name))}");
                    }
                }
                Console.WriteLine();
            }

            List<int> marks = student1.getMarks();
            Console.WriteLine($"\n{student1.Name}'s Marks: {string.Join(", ", marks)}");
            Console.WriteLine($"\n{student2.Name}'s Marks: {string.Join(", ", student2.getMarks())}");


            // Check if total == 20:
            if (student1 == 20)
            {
                Console.WriteLine($"\nTotal mark for {student1.Name} is 20.");
            }

            // Check if total != 20:
            if (student1 != 21)
            {
                Console.WriteLine($"Total mark for {student1.Name} is not 21.");
            }
        }
    }
}