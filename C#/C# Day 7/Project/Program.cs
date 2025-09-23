using System;
using System.IO.Compression;

namespace project
{
    public static class Extensions
    {
        public static int CountWords(this string phrase)
        {
            int count = 0;

            if (string.IsNullOrWhiteSpace(phrase))
                return count;

            string[] words = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            count += words.Length;

            return count;
        }

        public static bool IsEven(this int number)
        {
            return (number % 2 == 0) ? true : false;
        }

        public static int CalculateAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static string Reverse(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            char[] charArray = str.ToCharArray();
            int left = 0;
            int right = charArray.Length - 1;

            while (left < right)
            {
                (charArray[left], charArray[right]) = (charArray[right], charArray[left]);

                left++;
                right--;
            }

            return new string(charArray);
        }
    }
    internal class Project
    {
        public static object CreateProduct(string Name, int Price)
        {
            return new { Name = Name, Price = Price };
        }

        public static void PrintDetails(dynamic product)
        {
            string? name = product.Name;
            int? price = product.Price;
            Console.WriteLine("Product Info:");
            Console.WriteLine($"Product Name: {name}, Product Price: {price}$");
        }

        public static void Main(string[] args)
        {
            #region Anonymous objects

            dynamic product = CreateProduct("IPhone 14", 317);
            PrintDetails(product);
            
            #endregion

            #region Extention methods

            // 1
            string GreetMe = "Hello AbdElrhman!";
            int WordsCount = GreetMe.CountWords();
            Console.WriteLine($"Phrase: '{GreetMe}' has {WordsCount} words!");

            // 2
            int? x = 5;
            Console.WriteLine(x?.IsEven());
            x -= 1;
            Console.WriteLine(x?.IsEven());

            // 3
            DateTime birthDate = new DateTime(1990, 12, 1);
            int age = birthDate.CalculateAge();
            Console.WriteLine($"Birthdate: {birthDate.ToShortDateString()}");
            Console.WriteLine($"Today's Date: {DateTime.Today.ToShortDateString()}");
            Console.WriteLine($"Calculated Age: {age}");

            // 4
            string word = "Hello";
            string reversedWord = word.Reverse();
            Console.Write($"Before Reverse: {word}\nAfter Reverse: {reversedWord}\n");

            #endregion
        }
    }
}