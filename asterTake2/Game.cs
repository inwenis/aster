using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace asterTake2
{
    internal class Game
    {
        private const double FPS = 60;
        private readonly double _interval = TimeSpan.FromSeconds((1 / FPS)).Milliseconds;

        private readonly Form _window;
        private readonly Canvas _canvas;
        private readonly Thread _gameStateUpdatingThread;
        private bool _isRunning;
        private readonly Stopwatch _stopwatch;

        private readonly Ship _ship;
        private List<Asteroid> _asteroids;
        private List<Bullet> _bullets;

        private bool _isUpKeyPressed;
        private bool _isRightKeyPressed;
        private bool _isDownKeyPressed;
        private bool _isLeftKeyPressed;
        private bool _isShooting;
        private long _respawnStartTime;
        private long _respawnTime = 3000;
        private InputHandler _inputReader;
        private readonly Collider _collider;
        private int _level;
        private List<Bullet> _bulletsDead = new List<Bullet>();
        private List<Asteroid> _asteroidsDead = new List<Asteroid>();
        private long ActualFPS;
        private readonly Mover _mover;

        public Game()
        {
            _inputReader = new InputHandler();
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
            _canvas.Paint += _canvas_Paint;

            _stopwatch = new Stopwatch();

            _ship = ShipsAndAsteroidsCreator.CreateShip();
            _asteroids = ShipsAndAsteroidsCreator.CreateAsteroids(50);
            _bullets = new List<Bullet>();
            _collider = new Collider();
            _level = 1;
        }

        private void _canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            var drawFont = new Font("Arial", 16);
            var drawBrush = new SolidBrush(Color.Aquamarine);
            string message;
            if (_ship.Lives > 0)
            {
                message = "Lives: " + Enumerable.Repeat(" | ", _ship.Lives).Aggregate((a,n) => a + n);
                graphics.DrawString(message, drawFont, drawBrush, 10, 10);
            }
            else if(_ship.Lives == 0)
            {
                message = "Lives: 0";
                graphics.DrawString(message, drawFont, drawBrush, 10, 10);
            }
            else
            {
                message = "Lives: Game Over!";
                graphics.DrawString(message, drawFont, new SolidBrush(Color.Red), 10, 10);
            }
            
            graphics.DrawString("Asteroids: " + _asteroids.Count, drawFont, drawBrush, 10, 40);
            graphics.DrawString("Level: " + _level, drawFont, drawBrush, 10, 70);
            graphics.DrawString("Bullets: " + _bullets.Count, drawFont, drawBrush, 10, 100);
            graphics.DrawString("FPS: " + ActualFPS, drawFont, drawBrush, 10, 130);

            _ship.DrawShape(graphics);
            for (int index = 0; index < _bullets.Count; index++)
            {
                var bullet = _bullets[index];
                if (bullet.Alive)
                {
                    bullet.Draw(graphics);
                }
            }
            foreach (var asteroid in _asteroids.Where(a => a.Alive))
            {
                asteroid.Draw(graphics);
            }

            foreach (var asteroid in _asteroidsDead)
            {
//                asteroid.Draw(graphics);
            }
            foreach (var bullet in _bulletsDead)
            {
//                bullet.Draw(graphics);
            }
        }

        public void Start()
        {
            _stopwatch.Start();
            _isRunning = true;
            _window.Show();
            _window.Activate();
            while (_isRunning)
            {
                var start = _stopwatch.ElapsedMilliseconds;
                Application.DoEvents();

                var input = _inputReader.InputReader();
                if (input.UserRequestedToExit)
                {
                    _isRunning = false;
                }
                GameUpdate(input);
                
                _canvas.Invalidate();

                var end = _stopwatch.ElapsedMilliseconds;

                var timeTakienForFrame = end - start;

                var howManyFramesFitInOneSecond = 1000/timeTakienForFrame;

                ActualFPS = howManyFramesFitInOneSecond >= 60 ? 60 : howManyFramesFitInOneSecond;

                if (timeTakienForFrame < _interval)
                {
                    Thread.Sleep((int)(_interval - (_stopwatch.ElapsedMilliseconds - start)));
                }
                else
                {
                    Console.WriteLine("Game is running slowly");
                }
            }
        }

        private void GameWindowOnClosed(object sender, EventArgs eventArgs)
        {
            _isRunning = false;
        }

        private void GameUpdate(UserInput input)
        {
            if(input.UserRequestedDebug)
            {
                foreach (var asteroid in _asteroids)
                {
                    Console.WriteLine(asteroid.Position);
                }
            }

            if (_ship.IsAlive)
            {
                ShipMoverAndShooter.Move(_ship, input);
                ShipMoverAndShooter.HandleShooting(_ship, input, _bullets, _stopwatch.ElapsedMilliseconds);
            }
            _bulletsDead.AddRange(_bullets.Where(b => !b.Alive).ToList());
            _asteroidsDead.AddRange(_asteroids.Where(b => !b.Alive).ToList());

            _bullets = _bullets.Where(b => b.Alive).ToList();
            _asteroids = _asteroids.Where(a => a.Alive).ToList();

            foreach (var bullet in _bullets)
            {
                bullet.Move();
            }
            foreach (var asteroid in _asteroids)
            {
                _mover.Move(asteroid);
            }

            if (!_ship.IsRespawning)
            {
                _collider.HandleShipAsteroidCollisions(_asteroids, _ship, _stopwatch.ElapsedMilliseconds);
            }
            else
            {
                Console.WriteLine("Respawning... " + _ship.Lives);

                _ship.Hide = ((_stopwatch.ElapsedMilliseconds - _ship.RespawnStartTime)/150)%2 == 0;

                if (_stopwatch.ElapsedMilliseconds - _ship.RespawnStartTime >= _respawnTime)
                {
                    _ship.IsRespawning = false;
                    Console.WriteLine("Respawning ended...");
                }
            }

            Collider.HandleAsteroidBulletCollisions(_asteroids, _bullets);

            if (_asteroids.Count == 0)
            {
                _level += 1;
                int count = (int) (50 * Math.Pow(2, _level - 1));
                _asteroids = ShipsAndAsteroidsCreator.CreateAsteroids(count);
            }
        }
    }
}