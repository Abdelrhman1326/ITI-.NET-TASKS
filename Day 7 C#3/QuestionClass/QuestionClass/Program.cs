using System;

namespace program
{
    internal class Program
    {
        // single question
        class Question
        {
            public string Text { get; set; }
            public string[] Choices { get; set; }
            public int CorrectAnswer { get; set; }
            public int Mark { get; set; }

            public Question(string text, string[] choices, int correctAnswer, int mark)
            {
                Text = text;
                Choices = choices;
                CorrectAnswer = correctAnswer;
                Mark = mark;
            }

            public void Show()
            {
                Console.WriteLine($"\n{Text} (Mark: {Mark})");
                for (int i = 0; i < Choices.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Choices[i]}");
                }
            }

            public bool CheckAnswer(int answer) => answer == CorrectAnswer;
        }

        // exam (MCQ)
        class MCQ
        {
            public string Header { get; set; }   // Title of the exam
            public Question[] Body { get; set; } // Array of questions

            public MCQ(string header, Question[] body)
            {
                Header = header;
                Body = body;
            }

            public void Start()
            {
                Console.WriteLine($"\n{Header}\n");
                int total = 0, score = 0;

                foreach (var q in Body)
                {
                    q.Show();
                    Console.Write("Answer: ");
                    int answer = int.Parse(Console.ReadLine());

                    total += q.Mark;
                    if (q.CheckAnswer(answer))
                        score += q.Mark;
                }

                Console.WriteLine($"\nFinal Score: {score}/{total}");
            }
        }

        static void Main(string[] args)
        {
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

                Console.Write("How many choices? ");
                int c = int.Parse(Console.ReadLine());
                string[] choices = new string[c];

                for (int j = 0; j < c; j++)
                {
                    Console.Write($"Choice {j + 1}: ");
                    choices[j] = Console.ReadLine();
                }

                Console.Write("Correct choice number: ");
                int correct = int.Parse(Console.ReadLine());

                questions[i] = new Question(text, choices, correct, mark);
            }

            MCQ exam = new MCQ(examTitle, questions);
            exam.Start();
        }
    }
}