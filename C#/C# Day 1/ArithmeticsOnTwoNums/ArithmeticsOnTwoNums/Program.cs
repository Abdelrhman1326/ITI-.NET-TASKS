namespace program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("To do arithmetic press 1.");
                Console.WriteLine("To exit press 0.");

                char choice = '!';

                // Ensure choice is only '1' or '0'
                while (choice != '1' && choice != '0')
                {
                    choice = Console.ReadKey(intercept: true).KeyChar;
                }

                if (choice == '0')
                    break;

                long num1 = ReadLong("Enter the first number: ");

                long num2 = ReadLong("Enter the second number: ");

                Console.WriteLine("\n");
                Console.WriteLine($"first + second = {num1 + num2}");
                Console.WriteLine($"first - second = {num1 - num2}");
                Console.WriteLine($"first * second = {num1 * num2}");
                Console.Write("\n");
            }
        }
        static long ReadLong(string message)
        {
            long number;
            Console.Write(message);
            string input = Console.ReadLine();

            while (!long.TryParse(input, out number))
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                Console.Write(message);
                input = Console.ReadLine();
            }

            return number;
        }
    }
}