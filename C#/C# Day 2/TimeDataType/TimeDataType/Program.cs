namespace program
{
    class Time
    {
        // private fields:
        private int Hours;
        private int Minutes;
        private int Seconds;

        // public constructor:
        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        // Getter method:
        public string Get()
        {
            return $"{Hours}H:{Minutes}M:{Seconds}S";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // create new time object:
            Time time = new Time(22, 33, 11);
            string info = time.Get();
            Console.WriteLine(info);
        }
    }
}