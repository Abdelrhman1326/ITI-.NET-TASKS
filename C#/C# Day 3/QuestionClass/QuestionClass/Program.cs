namespace program
{
    internal class Program
    {
        // Calculator utility
        class Calc
        {
            public long Sum(long a, long b) => a + b;
            public double Sum(double a, double b) => a + b;
            public float Sum(float a, float b) => a + b;

            public long Sub(long a, long b) => a - b;
            public double Sub(double a, double b) => a - b;
            public float Sub(float a, float b) => a - b;

            public long Mul(long a, long b) => a * b;
            public double Mul(double a, double b) => a * b;
            public float Mul(float a, float b) => a * b;

            public long Div(long a, long b)
            {
                if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
                return a / b;
            }
            public double Div(double a, double b)
            {
                if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
                return a / b;
            }
            public float Div(float a, float b)
            {
                if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
                return a / b;
            }
        }

        // Single question
        class Question
        {
            public string Text { get; set; }
            public string[] Choices { get; set; }
            public double CorrectAnswer { get; private set; } // auto-calculated
            public int Mark { get; set; }
            public int UserAnswer { get; set; }

            private double Operand1;
            private double Operand2;
            private string Operator;
            private Calc calc;

            public Question(string text, string[] choices, double op1, double op2, string op, int mark, Calc calculator)
            {
                Text = text;
                Choices = choices;
                Operand1 = op1;
                Operand2 = op2;
                Operator = op;
                Mark = mark;
                UserAnswer = -1;
                calc = calculator;

                // Auto-calculate correct answer using Calc
                CorrectAnswer = Operator switch
                {
                    "+" => calc.Sum(Operand1, Operand2),
                    "-" => calc.Sub(Operand1, Operand2),
                    "*" => calc.Mul(Operand1, Operand2),
                    "/" => calc.Div(Operand1, Operand2),
                    _ => throw new Exception("Invalid operator")
                };
            }

            public void Show()
            {
                Console.WriteLine($"\n{Text} (Mark: {Mark})");
                for (int i = 0; i < Choices.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Choices[i]}");
                }
            }

            public bool IsCorrect() => UserAnswer == CorrectAnswer;
        }

        // Exam (MCQ)
        class MCQ
        {
            public string Header { get; set; }
            public Question[] Body { get; set; }

            public MCQ(string header, Question[] body)
            {
                Header = header;
                Body = body;
            }

            public void Start()
            {
                Console.WriteLine($"\n{Header}\n");
                foreach (var q in Body)
                {
                    q.Show();
                    Console.Write("Answer: ");
                    q.UserAnswer = int.Parse(Console.ReadLine());
                }
            }

            public void ShowResult()
            {
                Calc calc = new Calc();
                double total = 0, score = 0;

                foreach (var q in Body)
                {
                    total = calc.Sum(total, q.Mark);

                    if (q.IsCorrect())
                    {
                        score = calc.Sum(score, q.Mark);
                        Console.WriteLine($"Correct: {q.Text}");
                    }
                    else
                    {
                        Console.WriteLine($"Wrong: {q.Text} (Correct answer: {q.CorrectAnswer})");
                    }
                }

                Console.WriteLine($"\nFinal Score: {score}/{total}");
            }
        }

        static void Main(string[] args)
        {
            Calc calc = new Calc();

            Console.Write("Enter exam title: ");
            string examTitle = Console.ReadLine();

            Console.Write("How many questions? ");
            int n = int.Parse(Console.ReadLine());
            Question[] questions = new Question[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEnter Question {i + 1}: ");

                Console.Write("Text: ");
                string text = Console.ReadLine();

                Console.Write("Mark: ");
                int mark = int.Parse(Console.ReadLine());

                Console.Write("Operand 1: ");
                double op1 = double.Parse(Console.ReadLine());

                Console.Write("Operand 2: ");
                double op2 = double.Parse(Console.ReadLine());

                Console.Write("Operator (+, -, *, /): ");
                string op = Console.ReadLine();

                Console.Write("How many choices? ");
                int c = int.Parse(Console.ReadLine());
                string[] choices = new string[c];

                for (int j = 0; j < c; j++)
                {
                    Console.Write($"Choice {j + 1}: ");
                    choices[j] = Console.ReadLine();
                }

                // Correct answer auto-calculated
                questions[i] = new Question(text, choices, op1, op2, op, mark, calc);
            }

            MCQ exam = new MCQ(examTitle, questions);
            exam.Start();
            exam.ShowResult();
        }
    }
}