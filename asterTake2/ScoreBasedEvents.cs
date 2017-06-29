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
            if (scorePoints > 500)
            {
                ship.ShootingInterval = 100;
            }
            if (scorePoints > 1000)
            {
                ship.ShootingInterval = 50;
            }
        }
    }
}