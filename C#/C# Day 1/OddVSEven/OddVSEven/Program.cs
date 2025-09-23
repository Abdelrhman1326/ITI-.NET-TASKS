namespace program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To check even or odd press 1.");
                Console.WriteLine("To exit press 0.");
                char key = Console.ReadKey(intercept: true).KeyChar;

                if (key == '0')
                    break;

                Console.Write("Enter a number: ");
                string input = Console.ReadLine();

                // validate input
                long number;
                while (!long.TryParse(input, out number))
                {
                    Console.WriteLine("Invalid input!");
                    Console.Write("Please enter a valid number: ");
                    input = Console.ReadLine();
                }

                string result = (number % 2 == 0) ? "Even" : "Odd";
                Console.WriteLine($"This number is {result}\n");
            }
        }
    }
}