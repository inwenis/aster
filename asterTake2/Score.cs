using System.Collections.Generic;

namespace asterTake2
{
    internal class Score
    {
        public int Points;

        public void Add(List<Asteroid> destroyedAsteroids)
        {
            foreach (var destroyedAsteroid in destroyedAsteroids)
            {
                if (destroyedAsteroid.Generation == 2)
                {
                    Points += 10;
                }
                if (destroyedAsteroid.Generation == 1)
                {
                    Points += 20;
                }
                if (destroyedAsteroid.Generation == 0)
                {
                    Points += 30;
                }
            }
        }
    }
}