using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    public class Line : ComplexShape
    {
        static readonly Random Random = new Random();
        public double RotationSpeed;
        public Vector Velocity;

        public Line(Vector a, Vector b)
        {
            Shapes = new List<Shape>()
            {
                new Shape(a, b)
            };
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
