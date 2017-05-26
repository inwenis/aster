using System.Drawing;

namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives = 3;
        public bool IsRespawning = false;
        public bool IsAlive = true;
        public long RespawnStartTime { get; set; }
        public bool Hide;

        public override void DrawShape(Graphics g)
        {
            if (IsRespawning && Hide == false)
            {
                base.DrawShape(g);
            }
            if (!IsRespawning)
            {
                base.DrawShape(g);
            }
        }
    }
}