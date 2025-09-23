namespace program
{
    internal class Program
    {
        public interface IDrawable
        {
            void Draw();
        }
        // abstract class for all shapes:
        abstract class Shape : IDrawable
        {
            public abstract double Area();

            public virtual void Draw()
            {
                Console.Write("Drawing a Shape");
            }
        }


        // child classes "shapes":
        class Circle : Shape
        {
            public double Radius { get; set; }

            public override double Area()
            {
                return (Math.PI * Radius * Radius);
            }

            // Implement the Draw method from the IDrawable interface
            public override void Draw()
            {
                Console.Write("Drawing a Circle");
            }
        }

        class Square : Shape
        {
            public double Side { get; set; }

            public override double Area()
            {
                return (Side * Side);
            }

            public override void Draw()
            {
                Console.Write("Drawing a Square");
            }
        }

        class Rectangle : Shape, IDrawable
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public override double Area()
            {
                return (Width * Height);
            }

            public override void Draw()
            {
                Console.Write("Drawing a Rectangle");
            }
        }


        static void Main(string[] args)
        {
            // Create an array of Shape objects, demonstrating polymorphism
            Shape[] shapes = new Shape[3];
            shapes[0] = new Circle { Radius = 5.0 };
            shapes[1] = new Square { Side = 4.0 };
            shapes[2] = new Rectangle { Width = 3.0, Height = 6.0 };

            // Loop through the array and call the Area and Draw methods
            foreach (Shape shape in shapes)
            {
                shape.Draw();
                double area = shape.Area();
                Console.WriteLine($", Area: {area}");
            }
        }
    }
}