using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using asterTake2.ConsoleCommands;
using Size = System.Drawing.Size;

namespace asterTake2
{
    internal class Game
    {
        private long _actualFPS;
        private const double FPS = 60;
        private readonly double _interval = TimeSpan.FromSeconds(1 / FPS).Milliseconds;
        private readonly Form _window;
        private readonly Canvas _canvas;
        public bool IsRunning;
        private readonly Stopwatch _stopwatch;
        private Vector _shipStartingPoint;
        public readonly Ship Ship;
        public List<Asteroid> Asteroids;
        public List<Bullet> Bullets;
        private readonly Collider _collider;
        private int _level;
        private readonly Mover _mover;
        private readonly Score _score;
        private readonly ScoreBasedEvents _scoreBasedEvents;
        private readonly TimeBasedActions _timeBasedActions;
        private readonly List<IConsoleCommand> _commands;
        public bool ExitConsole;
        private List<Line> _lines = new List<Line>();
        private bool _scoreWasSaved = false;

        public Game()
        {
            _collider = new Collider();
            var mapWidth = 1000;
            var mapHeight = 600;
            _mover = new Mover(mapWidth, mapHeight);
            _window = new Form
            {
                FormBorderStyle = FormBorderStyle.Fixed3D, //what does this mean?
                ClientSize = new Size(mapWidth, mapHeight),
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                KeyPreview = false
            };

            _canvas = new Canvas();
            _window.Controls.Add(_canvas);

            _window.Closed += GameWindowOnClosed;
            _canvas.Paint += Paint;

            _stopwatch = new Stopwatch();

            _shipStartingPoint = new Vector(500, 300);
            Ship = new Ship(_shipStartingPoint);
            Asteroids = AsteroidAndBulletCreator.CreateAsteroids(5);
            Bullets = new List<Bullet>();
            _collider = new Collider();
            _level = 1;
            _score = new Score();
            _scoreBasedEvents = new ScoreBasedEvents();
            _timeBasedActions = new TimeBasedActions();

            var helpCommand = new HelpCommand();
            _commands = new List<IConsoleCommand>()
            {
                new ExitConsoleCommand(this),
                new ExitGameCommand(this),
                new CreateBulletCommand(this),
                new PrintAsteroidPositionsCommand(Asteroids),
                new Add10AsteroidsCommand(Asteroids),
                new RemoveLifesAndDestroyShipCommand(this),
                helpCommand
            };
            helpCommand.Commands = _commands;
        }

        private void Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var drawFont = new Font("Arial", 16);
            var drawBrush = new SolidBrush(Color.Aquamarine);
            string message;
            if (Ship.Lives > 0)
            {
                message = "Lives: " + Enumerable.Repeat(" | ", Ship.Lives).Aggregate((a,n) => a + n);
                graphics.DrawString(message, drawFont, drawBrush, 10, 10);
            }
            else if(Ship.Lives == 0)
            {
                message = "Lives: 0";
                graphics.DrawString(message, drawFont, drawBrush, 10, 10);
            }
            else
            {
                message = "Lives: Game Over!";
                graphics.DrawString(message, drawFont, new SolidBrush(Color.Red), 10, 10);
            }
            
            graphics.DrawString("Asteroids: " + Asteroids.Count, drawFont, drawBrush, 10, 40);
            graphics.DrawString("Level: " + _level, drawFont, drawBrush, 10, 70);
            graphics.DrawString("Bullets: " + Bullets.Count, drawFont, drawBrush, 10, 100);
            graphics.DrawString("FPS: " + _actualFPS, drawFont, drawBrush, 10, 130);
            graphics.DrawString("Score: " + _score.Points, drawFont, drawBrush, 10, 160);
            graphics.DrawString("Lines: " + _lines.Count, drawFont, drawBrush, 10, 190);

            if (!Ship.IsAlive)
            {
                graphics.DrawString("YOU ARE DEAD!", drawFont, drawBrush, _shipStartingPoint);
            }

            Ship.Draw(graphics);
            foreach (var bullet in Bullets.Where(b => b.Alive))
            {
                bullet.Draw(graphics);
            }
            foreach (var asteroid in Asteroids.Where(a => a.Alive))
            {
                asteroid.Draw(graphics);
            }
            foreach (var line in _lines)
            {
                line.Draw(graphics);
            }
        }

        public void Start()
        {
            _stopwatch.Start();
            IsRunning = true;
            _window.Show();
            _window.Activate();
            while (IsRunning)
            {
                var start = _stopwatch.ElapsedMilliseconds;
                Application.DoEvents();

                GameUpdate();

                _canvas.Invalidate();

                var end = _stopwatch.ElapsedMilliseconds;
                var timeTakienForFrame = end - start;
                var howManyFramesFitInOneSecond = 1000/timeTakienForFrame;
                _actualFPS = howManyFramesFitInOneSecond >= 60 ? 60 : howManyFramesFitInOneSecond;
                if (timeTakienForFrame < _interval)
                {
                    Thread.Sleep((int)(_interval - (_stopwatch.ElapsedMilliseconds - start)));
                }
                else
                {
                    Console.WriteLine("Game is running slowly");
                    Console.WriteLine("Press 'h' to see available commands");
                    Console.WriteLine("Press 'x' to enter console");
                }
            }
        }

        private void GameWindowOnClosed(object sender, EventArgs eventArgs)
        {
            IsRunning = false;
        }

        private void GameUpdate()
        {
            foreach (var consoleCommand in _commands.Where(x => Keyboard.IsKeyDown(x.ShortcutKey)))
            {
                consoleCommand.DoJobOnShortcutKey();
            }

            if (Keyboard.IsKeyDown(Key.X))
            {
                Console.WriteLine("entered console");
                ExitConsole = false;
                while (!ExitConsole)
                {
                    var textEnteredByUser = Console.ReadLine();
                    var command = _commands.SingleOrDefault(c => c.CanHandle(textEnteredByUser));
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

            if (Ship.IsAlive && !Ship.IsWaitingToBeRespawned)
            {
                _mover.Move(Ship);
                Ship.HandleShooting(Bullets, _stopwatch.ElapsedMilliseconds, Asteroids);
            }

            //TODO don't have to do this every frame
            Bullets = Bullets.Where(b => b.Alive).ToList();
            Asteroids.RemoveAll(IsDead);
            _lines = _lines
                .Where(IsInsideWindow)
                .Where(line => line.IsVisible())
                .ToList();

            //Move methods are a lie -> they set IsAlive too
            foreach (var bullet in Bullets)
            {
                _mover.Move(bullet);
            }
            foreach (var asteroid in Asteroids)
            {
                _mover.Move(asteroid);
            }
            foreach (var line in _lines)
            {
                _mover.Move(line);
            }

            if (!Ship.IsRespawning && !Ship.IsWaitingToBeRespawned)
            {
                var result = _collider.FindAsteroidCollidingWithShipIfAny(Asteroids, Ship, _stopwatch.ElapsedMilliseconds);
                if (result.Collision)
                {
                    HandleShipAsteroidCollision();
                }
            }
            if (Ship.IsAlive == false && _scoreWasSaved == false)
            {
                Console.WriteLine("you died");
                var enterScoreWindow = new EnterScoreWindow(_score.Points);
                enterScoreWindow.ShowDialog();
                using (var httpClient = new HttpClient())
                {
                    var result = httpClient.GetAsync($"http://localhost:49755/Home/AddScore?score={_score.Points}").Result;
                    Console.WriteLine(result);
                }

                _scoreWasSaved = true;
            }

            var destroyedAsteroids = Collider.HandleAsteroidBulletCollisions(Asteroids, Bullets);
            _score.Add(destroyedAsteroids);

            foreach (var destroyedAsteroid in destroyedAsteroids)
            {
                var lines = Line.GetLinesOfShapeFloatingInRandomDirections(destroyedAsteroid);
                _lines.AddRange(lines); //TODO clean _lines list
            }

            foreach (var destroyedAsteroid in Asteroids.Where(a => !a.Alive && a.Generation != 0).ToArray())
            {
                var newAsteroids = AsteroidAndBulletCreator.CreateSmallerAsteroids(destroyedAsteroid);
                Asteroids.AddRange(newAsteroids);
            }

            if (Asteroids.Count == 0)
            {
                _level += 1;
                int count = (int) (5 * Math.Pow(2, _level - 1));
                Asteroids.AddRange(AsteroidAndBulletCreator.CreateAsteroids(count));
            }

            _scoreBasedEvents.Handle(_score.Points, Ship);
            _timeBasedActions.Handle(_stopwatch.ElapsedMilliseconds);
        }

        private bool IsDead(Asteroid asteroid)
        {
            return asteroid.Alive == false;
        }

        private static bool IsInsideWindow(Line line)
        {
            if (line.Position.X < -100) return false;
            if (line.Position.X > 1100) return false;
            if (line.Position.Y < -100) return false;
            if (line.Position.Y > 700) return false;
            return true;
        }

        private void HandleShipAsteroidCollision()
        {
            if (Ship.Lives == 0)
            {
                Ship.IsAlive = false;
                Ship.IsVisible = false;
                return;
            }
            Ship.Lives -= 1;
            Console.WriteLine("you got hit! Lives left " + Ship.Lives);
            Ship.Velocity = new Vector();
            Ship.IsWaitingToBeRespawned = true;
            _timeBasedActions.ScheduleAction(5000, _stopwatch.ElapsedMilliseconds, StartRespawn);
            const int blinkIntervalMiliSeconds = 150;
            for (var i = 0; 5000 + i * blinkIntervalMiliSeconds < 8000; i++)
            {
                _timeBasedActions.ScheduleAction(5000 + i * blinkIntervalMiliSeconds, _stopwatch.ElapsedMilliseconds, () => Ship.IsVisible = !Ship.IsVisible);
            }
            _timeBasedActions.ScheduleAction(8000, _stopwatch.ElapsedMilliseconds, EndRespawn);

            var lines = Line.GetLinesOfShapeFloatingInRandomDirections(Ship);
            _lines.AddRange(lines);
        }

        private void EndRespawn()
        {
            Ship.IsRespawning = false;
        }

        private void StartRespawn()
        {
            Ship.IsRespawning = true;
            Ship.Position = _shipStartingPoint;
            Ship.IsWaitingToBeRespawned = false;
            var asteroidsThatAreTooClose = Asteroids
                .Where(a => (a.Position - _shipStartingPoint).Length < 300);
            foreach (var asteroid in asteroidsThatAreTooClose)
            {
                var vectorFromShipToAsteroid = asteroid.Position - _shipStartingPoint;
                vectorFromShipToAsteroid.Normalize();
                asteroid.Velocity = vectorFromShipToAsteroid;
            }
        }
    }
}