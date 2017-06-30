using System;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    public static class ExtensionMethods
    {
        public static Vector Rotate(this Vector @this, double radians, Vector rotationCenter = default(Vector))
        {
            var normalizedX = @this.X - rotationCenter.X;
            var normalizedY = @this.Y - rotationCenter.Y;

            var newX = normalizedX * Math.Cos(radians) - normalizedY * Math.Sin(radians);
            var newY = normalizedX * Math.Sin(radians) + normalizedY * Math.Cos(radians);

            return new Vector(newX + rotationCenter.X, newY + rotationCenter.Y);
        }

        public static void DrawEllipse(this Graphics graphics, Pen pen, double x, double y, double width, double height)
        {
            graphics.DrawEllipse(pen, (float) x, (float) y, (float) width, (float) height);
        }

        public static void FillRectangle(this Graphics graphics, Brush brush, double x, double y, double w, double h)
        {
            graphics.FillRectangle(brush, (float) x, (float) y, (float) w, (float) h);
        }

        public static void DrawLine(this Graphics @this, Pen pen, Vector a, Vector b)
        {
            @this.DrawLine(pen, (float) a.X, (float) a.Y, (float) b.X, (float) b.Y);
        }

        public static void DrawString(this Graphics @this, string text, Font font, Brush brush, Vector vector)
        {
            @this.DrawString(text, font, brush, (float) vector.X, (float) vector.Y);
        }

        public static void DrawPolygon(this Graphics @this, Pen pen, Vector[] vectors)
        {
            @this.DrawPolygon(pen, vectors.Select(v => v.ToPointF()).ToArray());
        }

        public static PointF ToPointF(this Vector @this)
        {
            return new PointF((float)@this.X, (float)@this.Y);
        }
    }
}
