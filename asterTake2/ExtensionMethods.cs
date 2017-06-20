using System;
using System.Drawing;
using System.Windows;

namespace asterTake2
{
    public static class ExtensionMethods
    {
        public static PointF Rotate(this PointF @this, double radians, PointF rotationCenter = default(PointF))
        {
            var normalizedX = @this.X - rotationCenter.X;
            var normalizedY = @this.Y - rotationCenter.Y;

            var newX = normalizedX * Math.Cos(radians) - normalizedY * Math.Sin(radians);
            var newY = normalizedX * Math.Sin(radians) + normalizedY * Math.Cos(radians);

            return new PointF((float)(newX + rotationCenter.X), (float)(newY + rotationCenter.Y));
        }

        public static Vector Rotate(this Vector @this, double radians)
        {
            var newX = @this.X * Math.Cos(radians) - @this.Y * Math.Sin(radians);
            var newY = @this.X * Math.Sin(radians) + @this.Y * Math.Cos(radians);

            return new Vector(newX, newY);
        }

        public static PointF Offset(this PointF @this, PointF offset)
        {
            return new PointF(@this.X + offset.X, @this.Y + offset.Y);
        }

        public static PointF Offset(this PointF @this, Vector shipVVelocity)
        {
            return new PointF((float) (@this.X + shipVVelocity.X), (float) (@this.Y + shipVVelocity.Y));
        }

        public static Vector ToVector(this PointF @this)
        {
            return new Vector(@this.X, @this.Y);
        }
    }
}