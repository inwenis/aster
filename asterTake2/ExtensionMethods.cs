using System;
using System.Drawing;
using System.Windows;

namespace asterTake2
{
    public static class ExtensionMethods
    {
        public static PointF Rotate(this PointF @this, double angle, PointF rotationCenter = default(PointF))
        {
            var normalizedX = @this.X - rotationCenter.X;
            var normalizedY = @this.Y - rotationCenter.Y;

            var newX = normalizedX * Math.Cos(angle) - normalizedY * Math.Sin(angle);
            var newY = normalizedX * Math.Sin(angle) + normalizedY * Math.Cos(angle);

            return new PointF((float)(newX + rotationCenter.X), (float)(newY + rotationCenter.Y));
        }

        public static PointF Offset(this PointF @this, PointF offset)
        {
            return new PointF(@this.X + offset.X, @this.Y + offset.Y);
        }

        public static Vector Rotate(this Vector @this, double angle)
        {
            var newX = @this.X * Math.Cos(angle) - @this.Y * Math.Sin(angle);
            var newY = @this.X * Math.Sin(angle) + @this.Y * Math.Cos(angle);

            return new Vector(newX, newY);
        }

        public static PointF Offset(this PointF @this, Vector shipVVelocity)
        {
            return new PointF((float) (@this.X + shipVVelocity.X), (float) (@this.Y + shipVVelocity.Y));
        }
    }
}