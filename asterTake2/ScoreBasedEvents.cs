namespace asterTake2
{
    internal class ScoreBasedEvents
    {
        public void Handle(int scorePoints)
        {
            if (ShipMoverAndShooter.AutoAimBullets == false && scorePoints > -1)
            {
                ShipMoverAndShooter.AutoAimBullets = true;
            }
        }
    }
}