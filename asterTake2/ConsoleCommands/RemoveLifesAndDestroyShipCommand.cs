using System.Windows;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    internal class RemoveLifesAndDestroyShipCommand : IConsoleCommand
    {
        private Game _game;

        public RemoveLifesAndDestroyShipCommand(Game game)
        {
            _game = game;
        }

        public Key ShortcutKey => Key.K;

        public bool CanHandle(string input)
        {
            return input == "kill";
        }

        public void DoJob(string input)
        {
            _game.Ship.Lives = 0;
            var asteroid = AsteroidAndBulletCreator.CreateAsteroid();
            asteroid.Position = _game.Ship.Position;
            _game.Asteroids.Add(asteroid);
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "kill - remove all lifes and destroy ship";
        }
    }
}