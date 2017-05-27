using System.Drawing;

namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives = 3;
        public bool IsRespawning = false;
        public bool IsAlive = true;
        public long RespawnStartTime { get; set; }
        public PointF Velocity { get; set; }

        public int Radius;

        public bool Hide;

        public void DrawShape(Graphics graphics)
        {
            if (IsRespawning && Hide == false)
            {
                foreach (var shape in Shapes)
                {
                    shape
                        .Rotate(Angle, RotationCenter)
                        .Offset(Position)
                        .Draw(graphics);
                }
            }
            if (!IsRespawning)
            {
                foreach (var shape in Shapes)
                {
                    shape
                        .Rotate(Angle, RotationCenter)
                        .Offset(Position)
                        .Draw(graphics);
                }
            }
            float radius = 33;
            graphics.DrawEllipse(Pens.Red, Position.X - radius, Position.Y - radius, 2 * radius, 2 * radius);
        }
    }
}