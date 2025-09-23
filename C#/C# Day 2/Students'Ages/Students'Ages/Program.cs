using System;
using System.Collections.Generic;
using System.Linq;

namespace program
{
    internal class Project
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter the number of departments: ");
            int numberOfDep = Convert.ToInt32(Console.ReadLine());

            // Map: Department -> List of student ages
            Dictionary<string, List<int>> departmentData = new Dictionary<string, List<int>>();

            for (int i = 0; i < numberOfDep; i++)
            {
                Console.Write($"Enter name of department {i + 1}: ");
                string depName = Console.ReadLine();

                Console.Write($"Enter the number of students in {depName}: ");
                int studCount = Convert.ToInt32(Console.ReadLine());

                // Create a list to hold ages
                List<int> ages = new List<int>();

                for (int j = 0; j < studCount; j++)
                {
                    Console.Write($"Enter the age of student {j + 1} in {depName}: ");
                    int age = Convert.ToInt32(Console.ReadLine());
                    ages.Add(age);
                }

                // Store in dictionary
                departmentData[depName] = ages;
            }

            // Display the data
            Console.WriteLine("\n=== Department Data ===");
            foreach (var dep in departmentData)
            {
                Console.WriteLine($"Department: {dep.Key}");
                Console.WriteLine("Student Ages: " + string.Join(", ", dep.Value));

                double avgAge = dep.Value.Average();
                Console.WriteLine($"Average Age: {avgAge:F2}\n");
            }
        }
    }
}