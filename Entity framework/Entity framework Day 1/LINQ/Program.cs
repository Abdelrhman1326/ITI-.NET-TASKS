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
            Console.WriteLine(result);
        }
    }
}