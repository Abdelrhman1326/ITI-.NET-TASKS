namespace program
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of the table you want: ");
            long num = long.Parse(Console.ReadLine());

            Console.WriteLine($"\nMultiplication Table of {num}:");
            for (int i = 1; i <= 12; i++)
            {
                Console.WriteLine($"{num} x {i} = {num * i}");
            }
        }
    }
}