using System;
using System.Collections.Generic;
using System.Linq;

namespace project
{
    internal class Project
    {
        public static void Main(string[] args)
        {
            #region 
            // list
            List<int> numbers = new List<int>() { 2, 4, 6, 7, 1, 4, 2, 9, 1 };


            // Query 1: Display numbers without any repeated Data and sorted
            var uniqueSortedNumbers = numbers.Distinct().OrderBy(n => n);
            string result0 = string.Join(", ", uniqueSortedNumbers);
            Console.WriteLine(result0 + '\n');


            // Query2: using Query1 result and show each number and it’s multiplication
            Dictionary<int, long> PowOfTwo = new Dictionary<int, long>();
            foreach (int number in uniqueSortedNumbers)
            {
                PowOfTwo[number] = (long)((long)number * (long)number);
            }
            foreach (var pair in PowOfTwo)
            {
                Console.WriteLine($"( Number = {pair.Key}, Multiply = {pair.Value} )" + '\n');
            }
            #endregion

            #region 
            // array:
            string[] names = ["Tom", "Dick", "Harry", "MARY", "Jay"];


            // Query1: Select names with length equal 3.
            var namesOfLengthThree = names.Where(name => name.Length == 3);
            string result1 = string.Join(", ", namesOfLengthThree);
            Console.WriteLine(result1 + '\n');

            #endregion
        }
    }
}