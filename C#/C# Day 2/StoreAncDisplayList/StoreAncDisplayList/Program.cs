namespace project
{
    internal class Project
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of students: ");
            int num = Convert.ToInt32(Console.ReadLine());
            string[] students = new string[num];

            // First loop: fill array with names
            for (int i = 0; i < num; i++)
            {
                Console.Write($"Enter student {i + 1} name: ");
                students[i] = Console.ReadLine();
            }

            Console.WriteLine("\n--- Student List ---");

            // Second loop: print names (using foreach)
            foreach (string student in students)
            {
                Console.WriteLine(student);
            }
        }
    }
}