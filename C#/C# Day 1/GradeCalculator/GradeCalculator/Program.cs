namespace program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your degree (0-100): ");
            int degree = int.Parse(Console.ReadLine());
            string grade = GetGrade(degree);
            Console.WriteLine($"Your grade is: {grade}");
        }

        static string GetGrade(int degree)
        {
            if (degree >= 97 && degree <= 100)
                return "A+";
            else if (degree >= 93 && degree <= 96)
                return "A";
            else if (degree >= 90 && degree <= 92)
                return "A-";
            else if (degree >= 87 && degree <= 89)
                return "B+";
            else if (degree >= 83 && degree <= 86)
                return "B";
            else if (degree >= 80 && degree <= 82)
                return "B-";
            else if (degree >= 77 && degree <= 79)
                return "C+";
            else if (degree >= 73 && degree <= 76)
                return "C";
            else if (degree >= 70 && degree <= 72)
                return "C-";
            else if (degree >= 67 && degree <= 69)
                return "D+";
            else if (degree >= 65 && degree <= 66)
                return "D";
            else
                return "F";
        }
    }
}