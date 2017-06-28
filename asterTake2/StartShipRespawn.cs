using System.Drawing;

namespace asterTake2
{
    internal class StartShipRespawn : IEvent
    {
        private readonly Ship _ship;
        private readonly PointF _shipStartingPoint;

        public StartShipRespawn(Ship ship, PointF shipStartingPoint)
        {
            _ship = ship;
            _shipStartingPoint = shipStartingPoint;
        }

        public void Run(long currentMillisecond)
        {
            _ship.IsRespawning = true;
            _ship.Position = _shipStartingPoint;
            _ship.IsWaitingToBeRespawned = false;
            _ship.RespawnStartTime = currentMillisecond;
        }
    }
}