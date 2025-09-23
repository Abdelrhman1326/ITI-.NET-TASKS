using System.Collections;
using System.Runtime.CompilerServices;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            #region Bubble sort
            /* 
                1- Bubble sort algorithm can reach worst case of O(N**2) if we did it
                with a non-optimized nested loop, but we can optimize it by limiting the max index
                reached by the innder loop cause bubble sort plays on arranging the elements descendingly
                from rhs to lhs, so in each new inner loop, we are sure that a new num gets blugged into it's 
                correct place at the end
            */

            // read list:
            string? input = Console.ReadLine();
            // split by spaces and assing to List:
            string[] splitInput = input?.Split(' ') ?? Array.Empty<string>();
            List<int> nums = new List<int>();

            foreach (string s in splitInput)
            {
                if (int.TryParse(s, out int number))
                {
                    nums.Add(number);
                }
            }

            // apply bubble sort:
            for (int i = 0; i < nums.Count(); i++)
            {
                for (int j = 0; j < nums.Count() - i - 1; j++)
                {
                    if (nums[j + 1] < nums[j])
                    {
                        (nums[j], nums[j + 1]) = (nums[j + 1], nums[j]);
                    }
                }
            }
            // print nums:
            string sorted = string.Join(" ", nums);
            Console.WriteLine(sorted);
            #endregion

            #region generic range T Class
            /*
                2. create a generic Range<T> class that represents a range of values from a
                minimum value to a maximum value. The range should support basic
                operations such as checking if a value is within the range and
                determining the length of the range.
                Requirements:
                1. Create a generic class named Range<T> where T represents the type
                of values.
                2. Implement a constructor that takes the minimum and maximum
                values to define the range.
                3. Implement a method IsInRange(T value) that returns true if the given
                value is within the range, otherwise false.
                4. Implement a method Length() that returns the length of the range
                (the difference between the maximum and minimum values).
                5. Note: You can assume that the type T used in the Range<T> class
                implements the IComparable<T> interface to allow for comparisons.
            */

            // create new Range instance:
            Range<int> range = new Range<int>(1, 10); // call the constructor, pass min and max arguments
            // try get max and min:
            var max = range.Max;
            var min = range.Min;
            Console.WriteLine($"Min: {min}, Max: {max}");
            // try is in range:
            int num1 = 20;
            int num2 = 5;
            Console.WriteLine(range.IsInRange(num1) ? $"num1:{num1} is in range" : $"num1:{num1} is not in range");
            Console.WriteLine(range.IsInRange(num2) ? $"num2:{num2} is in range" : $"num2:{num2} is not in range");

            // try length property:
            double length = range.Length;
            Console.WriteLine($"Length of range is: {length}");

            // try count property (get number of nums within the range):
            double count = range.Count;
            Console.WriteLine($"Count of nums within the range: {count}");
            #endregion

            #region ArrayList reversing
            /*
                3. You are given an ArrayList containing a sequence of elements. try to
                reverse the order of elements in the ArrayList in-place(in the same
                arrayList) without using the built-in Reverse. Implement a function that
                takes the ArrayList as input and modifies it to have the reversed order of
                elements.
            */

            // take an array list as an input:
            string[] input1 = (Console.ReadLine() ?? string.Empty).Split(" ");
            // map to an array list:
            ArrayList arrayList = new ArrayList(); // remeber: arraylists accepts objects only (not strongly typed)
            // split the input:
            foreach (string s in input1)
            {
                // boxing:
                object objectVal = (object)s;
                // push to the list:
                arrayList.Add(objectVal);
            }

            // print array list before reversing:
            Console.WriteLine("ArrayList before reverse:");
            foreach (object val in arrayList)
            {
                Console.Write(val);
                Console.Write(" ");
            }
            // pass the ArrayList to the helper function to reverse it:
            ArrayListHelper.Reverse(ref arrayList);

            // print array list after reversing:
            Console.WriteLine("ArrayList after reverse:");
            foreach (object val in arrayList)
            {
                Console.Write(val);
                Console.Write(" ");
            }
            #endregion

            #region ExtractEven
            /*
                4. You are given a list of integers. Your task is to find and return a new list
                containing only the even numbers from the given list.
            */

            // take an array list as an input:
            string[] input2 = (Console.ReadLine() ?? string.Empty).Split(" ");
            // map to an array list:
            List<double> list = new List<double>();
            // split the input:
            foreach (string s in input2)
            {
                // push to the list:
                if (double.TryParse(s, out double number))
                {
                    list.Add(number);
                }
            }

            List<double> evens = new List<double>();
            foreach (double number in list)
            {
                if (number % 2 == 0)
                {
                    evens.Add(number);
                }
            }
            Console.Write("All numbers in list: ");
            string FullList = string.Join(", ", list);
            Console.WriteLine(FullList);

            Console.Write("Even numbers in the list: ");
            string evenNums = string.Join(", ", evens);
            Console.WriteLine(evenNums);

            #endregion

            #region Fixed Size list

            /*
                5. implement a custom list called FixedSizeList<T> with a predetermined
                capacity. This list should not allow more elements than its capacity and

                should provide clear messages if one tries to exceed it or access invalid
                indices.
                Requirements:
                1. Create a generic class named FixedSizeList<T>.
                2. Implement a constructor that takes the fixed capacity of the list as a
                parameter.
                3. Implement an Add method that adds an element to the list, but
                throws an exception if the list is already full.
                4. Implement a Get method that retrieves an element at a specific index
                in the list but throws an exception for invalid indices.
            */

            FixedSizeList<int> numbers = new FixedSizeList<int>(3);

            numbers.Add(10);
            numbers.Add(20);
            numbers.Add(30);

            Console.WriteLine(numbers.Get(1)); // prints 20

            // This will throw: list is full
            // numbers.Add(40);

            // This will throw: invalid index
            // Console.WriteLine(numbers.Get(5));

            #endregion

            #region String methods

            /*
                6. Given a string, find the first non-repeated character in it and return its
                index. If there is no such character, return -1. Hint you can use dictionary
            */

            Console.WriteLine(FirstUniqueChar("programming"));   // 0 (l)
            Console.WriteLine(FirstUniqueChar("ILoveC#")); // 2 (v)
            Console.WriteLine(FirstUniqueChar("aabb"));      // -1

            #endregion
        }
        static int FirstUniqueChar(string s)
        {
            if (string.IsNullOrEmpty(s)) 
                return -1;

            // Count occurrences
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (counts.ContainsKey(c))
                    counts[c]++;
                else
                    counts[c] = 1;
            }

            // Find first character with count = 1
            for (int i = 0; i < s.Length; i++)
            {
                if (counts[s[i]] == 1)
                    return i;
            }

            return -1;
        }
    }
    class Range<T> where T : IComparable<T>
    {
        private T _min;
        private T _max;

        public T Min
        {
            get { return _min; }
        }
        public T Max
        {
            get { return _max; }
        }

        // constructor:
        public Range(T min, T max)
        {
            if (min.CompareTo(max) > 0)
            {
                throw new ArgumentException("Minimum value can't be greater than Maximum value");
            }
            _min = min;
            _max = max;
        }

        // IsInRange:
        public bool IsInRange(T value)
        {
            bool isGreaterThanMin = value.CompareTo(_min) >= 0;
            bool isLessThanMax = value.CompareTo(_max) <= 0;
            return (isGreaterThanMin && isLessThanMax);
        }
        // length of the range:
        public double Length
        {
            get
            {
                dynamic dMin = _min;
                dynamic dMax = _max;
                return dMax - dMin;
            }
        }
        public double Count
        {
            get
            {
                dynamic dMin = _min;
                dynamic dMax = _max;
                return dMax - dMin + 1;
            }
        }
    }

    public class ArrayListHelper
    {
        public static void Reverse(ref ArrayList arrayList)
        {
            int left = 0;
            int right = arrayList.Count - 1;

            while (left < right)
            {
                // Swap elements
                (arrayList[left], arrayList[right]) = (arrayList[right], arrayList[left]);

                left++;
                right--;
            }
        }
    }

    public class FixedSizeList<T>
    {
        private readonly T[] _items;
        private int _count;

        // Constructor: set capacity
        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            _items = new T[capacity];
            _count = 0;
        }

        // Add method: adds an element, throws if full
        public void Add(T item)
        {
            if (_count >= _items.Length)
                throw new InvalidOperationException("Cannot add more elements: the list is full.");

            _items[_count++] = item;
        }

        // Get method: retrieves element by index, throws if invalid
        public T Get(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException("Invalid index: no element at this position.");

            return _items[index];
        }

        // Optional: property for current count
        public int Count => _count;

        // Optional: property for max capacity
        public int Capacity => _items.Length;
    }
}