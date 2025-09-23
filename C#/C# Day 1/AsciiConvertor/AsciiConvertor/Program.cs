namespace program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool repeat = true;
            while (repeat)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nTo convert from ASCII to Char press 1.");
                Console.WriteLine("To convert from Char to ASCII press 2.");
                Console.WriteLine("To exit press 0.");

                // Key press "listener"
                bool wrongInput = true;
                char key;
                do
                {
                    key = Console.ReadKey(intercept: true).KeyChar;
                    if (key == '1' || key == '2' || key == '0')
                        wrongInput = false;
                } while (wrongInput);

                switch (key)
                {
                    case '0':
                        repeat = false;
                        Console.WriteLine("\nExiting...");
                        break;

                    case '1':
                        Console.Write("\nYou chose ASCII -> CHAR\nPlease enter an ASCII code (number): ");
                        Console.ForegroundColor = ConsoleColor.White;
                        string asciiInput = Console.ReadLine();
                        if (int.TryParse(asciiInput, out int asciiCode))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Char = {(char)asciiCode}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! Please enter a valid number.");
                        }
                        break;

                    case '2':
                        Console.Write("\nYou chose CHAR -> ASCII\nPlease enter a Character: ");
                        string charInput = Console.ReadLine();
                        if (char.TryParse(charInput, out char character))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"ASCII = {(int)character}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input! Please enter a single character.");
                        }
                        break;
                }
            }
        }
    }
}