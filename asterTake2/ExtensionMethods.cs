using System;
using System.Drawing;

namespace asterTake2
{
    public static class ExtensionMethods
    {
        public static PointF Rotate(this PointF @this, double angle, PointF rotationCenter)
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

    }
}