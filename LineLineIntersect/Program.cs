using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineLineIntersect
{
    class Program
    {
        /*
         * Sovs: https://stackoverflow.com/questions/385305/efficient-maths-algorithm-to-calculate-intersections
         * 
float x12 = x1 - x2;
float y12 = y1 - y2;
float x34 = x3 - x4;
float y34 = y3 - y4;

float c = x12 * y34 - y12 * x34;

if (fabs(c) < 0.01)
{
  // No intersection
  return false;
}
else
{
  // Intersection
  float a = x1 * y2 - y1 * x2;
  float b = x3 * y4 - y3 * x4;

  float x = (a * x34 - b * x12) / c;
  float y = (a * y34 - b * y12) / c;

  return true;
}
         */

        static void Main(string[] args)
        {
            // Examples.
            List<Line[]> lines = new List<Line[]>(new[]
            {
                new []
                {   // Example with two lines that overlap and intersect (4,2).
                    new Line(3, 1, 5, 3),
                    new Line(5, 1, 3, 3)
                },
                new[]
                {   // Example with two lines that doesn't overlap but will intersect.
                    new Line(1, 3, 6, 5),
                    new Line(9, 1, 8, 4)
                },
                new[]
                {   // Example with two lines that will never intersect.
                    new Line(1, 2, 6, 9),
                    new Line(1, 1, 6, 8)
                }
            });

            // Set current example lines.
            Line l1 = lines[0][0];
            Line l2 = lines[0][1];

            // Get intersection.
            Point intersection = l1.IntersectsWith(l2);

            if (intersection != null)
            {
                Console.WriteLine("Intersection: ({0},{1})", intersection.X, intersection.Y);
            }

            Console.ReadKey();
        }
    }

    class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Point Vector { get { return new Point(Start.X - End.X, Start.Y - End.Y); } }
        public float Cross { get { return (Start.X * End.Y) - (Start.Y * End.X); } }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line(float startX, float startY, float endX, float endY)
        {
            Start = new Point(startX, startY);
            End = new Point(endX, endY);
        }

        public Point IntersectsWith(Line other)
        {
            Console.WriteLine("Line A: ({0},{1}) ({2},{3})", Start.X, Start.Y, End.X, End.Y);
            Console.WriteLine("Line B: ({0},{1}) ({2},{3})", other.Start.X, other.Start.Y, other.End.X, other.End.Y);

            float c = (Vector.X * other.Vector.Y) - (Vector.Y * other.Vector.X);

            if (Math.Abs(c) < 0.01)
            {
                Console.WriteLine("Lines will never intersect!");
                return null;
            }

            float a = Cross;
            float b = other.Cross;

            float x = ((Cross * other.Vector.X) - (other.Cross * Vector.X)) / c;
            float y = ((Cross * other.Vector.Y) - (other.Cross * Vector.Y)) / c;

            return new Point(x, y);
        }
    }

    class Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
