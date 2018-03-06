using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    public class Line : ComplexShape
    {
        static readonly Random Random = new Random();
        private DateTimeOffset _createdDateTime;
        private int _timeToLive = 5;

        public Line(Vector a, Vector b)
        {
            Shapes = new List<Shape>()
            {
                new Shape(a, b)
            };
            _createdDateTime = DateTimeOffset.UtcNow;
        }

        public override void Draw(Graphics graphics)
        {
            var start = 0;
            var end = 255;
            var timePassedTotalSeconds = (DateTimeOffset.UtcNow - _createdDateTime).TotalSeconds;
            if (timePassedTotalSeconds > _timeToLive)
                return;
            var alpha = end - (timePassedTotalSeconds / _timeToLive * end);
            Console.WriteLine(alpha);
            var pen = new Pen(Color.FromArgb((int)alpha, Color.BlueViolet));
            //wtf -> pen is idosposable?
            foreach (var shape in Shapes)
            {
                shape
                    .Rotate(AngleRadians, RotationCenter)
                    .Offset(Position)
                    //set dynamic alpha color
                    .Draw(graphics, pen);
            }
        }

        public static List<Line> GetLinesOfShapeFloatingInRandomDirections(ComplexShape complexShape)
        {
            var lines = new List<Line>();
            foreach (var shape in complexShape.Shapes)
            {
                var points = shape.Points;
                for (var i = 0; i < points.Length; i++)
                {
                    Vector a;
                    Vector b;
                    if (i == points.Length - 1)
                    {
                        a = points.Last();
                        b = points.First();
                    }
                    else
                    {
                        a = points[i];
                        b = points[i + 1];
                    }
                    a = a.Rotate(complexShape.AngleRadians, complexShape.RotationCenter);
                    b = b.Rotate(complexShape.AngleRadians, complexShape.RotationCenter);
                    var center = (a + b) / 2;
                    var line = new Line(a, b)
                    {
                        Position = complexShape.Position,
                        RotationCenter = center,
                        Velocity = new Vector(Random.Next(10) - 5, Random.Next(10) - 5),
                        RotationSpeed = (Random.NextDouble() - 0.5) * Math.PI / 12
                    };
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
