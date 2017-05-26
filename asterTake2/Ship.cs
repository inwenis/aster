namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives = 3;
        public bool IsRespawning = false;
        public bool IsAlive = true;
        public long RespawnStartTime { get; set; }
    }
}