namespace asterTake2
{
    internal class ScoreBasedEvents
    {
        public void Handle(int scorePoints)
        {
            if (ShipMoverAndShooter.AutoAimBullets == false && scorePoints > 100)
            {
                ShipMoverAndShooter.AutoAimBullets = true;
            }
        }
    }
}