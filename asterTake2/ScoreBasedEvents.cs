namespace asterTake2
{
    internal class ScoreBasedEvents
    {
        public void Handle(int scorePoints, Ship ship)
        {
            if (ship.HasAutoAimBullets == false && scorePoints > 100)
            {
                ship.HasAutoAimBullets = true;
            }
        }
    }
}