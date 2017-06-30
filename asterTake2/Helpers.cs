using System;
using System.Windows;

namespace asterTake2
{
    public static class Helpers
    {
        public static double AngleBetweenRadians(Vector a, Vector b)
        {
            return Vector.AngleBetween(a, b) * Math.PI / 180;
        }
    }
}
