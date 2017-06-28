namespace asterTake2
{
    internal class EndShipRespawn : IEvent
    {
        private readonly Ship _ship;

        public EndShipRespawn(Ship ship)
        {
            _ship = ship;
        }

        public void Run()
        {
            _ship.IsRespawning = false;
        }
    }
}