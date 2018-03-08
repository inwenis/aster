using System;
using System.Collections.Generic;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    public class PrintAsteroidPositionsCommand : IConsoleCommand
    {
        private readonly List<Asteroid> _asteroids;

        public PrintAsteroidPositionsCommand(List<Asteroid> asteroids)
        {
            _asteroids = asteroids;
        }

        public Key ShortcutKey => Key.D;

        public bool CanHandle(string input)
        {
            return input == "printAsteroidPositions";
        }

        public void DoJob(string input)
        {
            foreach (var asteroid in _asteroids)
            {
                Console.WriteLine(asteroid.Position);
            }
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "printAsteroidPositions - print positions of asteroids";
        }
    }
}