using System.Drawing;
using System.Windows;

namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives = 3;
        public bool IsRespawning = false;
        public bool IsAlive = true;
        public long RespawnStartTime { get; set; }
        public bool IsWaitingToBeRespawned;

        public int Radius;
        public bool Hide;

        public Vector Acceleration = new Vector(0, -0.1);
        public Vector Velocity;
        public Vector MaxVelocity = new Vector(3, 0);

        public void DrawShape(Graphics graphics)
        {
            float radius = 33;
            if (!IsWaitingToBeRespawned && !Hide)
            {
                graphics.DrawEllipse(Pens.Red, Position.X - radius, Position.Y - radius, 2 * radius, 2 * radius);
                foreach (var shape in Shapes)
                {
                    shape
                        .Rotate(Angle, RotationCenter)
                        .Offset(Position)
                        .Draw(graphics);
                }
            }
            else if (IsWaitingToBeRespawned)
            {
                graphics.DrawEllipse(Pens.Aqua, Position.X - radius, Position.Y - radius, 2 * radius, 2 * radius);
            }
        }
    }
}