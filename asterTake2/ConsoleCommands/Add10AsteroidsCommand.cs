using System.Collections.Generic;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    public class Add10AsteroidsCommand : IConsoleCommand
    {
        private readonly List<Asteroid> _asteroids;

        public Add10AsteroidsCommand(List<Asteroid> asteroids)
        {
            _asteroids = asteroids;
        }

        public Key ShortcutKey => Key.A;

        public bool CanHandle(string input)
        {
            return input == "10asteroids";
        }

        public void DoJob(string input)
        {
            var asteroids = AsteroidAndBulletCreator.CreateAsteroids(10);
            _asteroids.AddRange(asteroids);
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "10asteroids - add 10 random asteroids";
        }
    }
}