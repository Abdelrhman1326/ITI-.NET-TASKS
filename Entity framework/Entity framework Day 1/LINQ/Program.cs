namespace project
{
    internal class Project
    {
        public static void Main(string[] args)
        {
            // list
            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };

            // Query 1: Display numbers without any repeated Data and sorted
            var uniqueSortedNumbers = numbers.Distinct().OrderBy(n => n);
            // test query 1:
            string result = string.Join(", ", uniqueSortedNumbers);
            Console.WriteLine(result + '\n');

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
        }
    }
}