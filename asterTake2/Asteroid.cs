namespace asterTake2
{
    internal class Asteroid : ComplexShape
    {
        public bool Alive;

        public Asteroid() : base()
        {
            Alive = true;
        }

        public void MarkDead()
        {
            Alive = false;
        }
    }
}