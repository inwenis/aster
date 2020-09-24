using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    internal class DebugKeysHandler
    {
        private readonly Game _game;
        public List<IConsoleCommand> Commands;

        public DebugKeysHandler(Game game)
        {
            _game = game;
            Commands = new List<IConsoleCommand>()
            {
                new ExitCommand(game),
                new CreateBulletCommand(game),
                new CreateAutoAimBulletOnKeyB(new CreateBulletCommand(game))
            };
        }

        public void HandleDebugKeys()
        {
            if (Keyboard.IsKeyDown(Key.D))
            {
                foreach (var asteroid in _game.Asteroids)
                {
                    Console.WriteLine(asteroid.Position);
                }
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                var asteroids = AsteroidAndBulletCreator.CreateAsteroids(10);
                _game.Asteroids.AddRange(asteroids);
            }
            if (Keyboard.IsKeyDown(Key.B))
            {
                var asteroid = AsteroidAndBulletCreator.CreateAsteroid(3);
                _game.Asteroids.Add(asteroid);
            }
            if (Keyboard.IsKeyDown(Key.C))
            {
                foreach (var asteroid in _game.Asteroids)
                {
                    asteroid.MarkDead();
                    var lines = Line.GetLinesOfShapeFloatingInRandomDirections(asteroid);
                    _game.Lines.AddRange(lines);
                }
            }
            if (Keyboard.IsKeyDown(Key.X))
            {
                Console.WriteLine("entered console");
                _game.ExitConsole = false;
                while (!_game.ExitConsole)
                {
                    var textEnteredByUser = Console.ReadLine();
                    var command = Commands.SingleOrDefault(c => c.CanHandle(textEnteredByUser));
                    if (command != default(IConsoleCommand))
                    {
                        command.DoJob(textEnteredByUser);
                    }
                    else
                    {
                        Console.WriteLine("unknown command");
                    }
                }
            }
        }
    }
}