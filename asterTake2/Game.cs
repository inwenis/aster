using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

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

        private readonly ComplexShape _ship;
        private readonly List<ComplexShape> _asteroids;
        private readonly List<Bullet> _bullets;

        private bool _isUpKeyPressed;
        private bool _isRightKeyPressed;
        private bool _isDownKeyPressed;
        private bool _isLeftKeyPressed;
        private bool _isShooting;

        public Game()
        {
            _window = new Form
            {
                FormBorderStyle = FormBorderStyle.Fixed3D, //what does this mean?
                ClientSize = new Size(1000, 600),
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                KeyPreview = false
            };

            _canvas = new Canvas();
            _window.Controls.Add(_canvas);

            _window.KeyDown += GameWindowOnKeyDown;
            _window.KeyUp += WindowOnKeyUp;
            _window.Closed += GameWindowOnClosed;

            _canvas.Paint += GameDraw;

            _stopwatch = new Stopwatch();
            _gameStateUpdatingThread = new Thread(MainLoop);

            _ship = ShipsAndAsteroidsCreator.CreateShip();
            _asteroids = ShipsAndAsteroidsCreator.CreateAsteroids();
            _bullets = new List<Bullet>();
        }

        public void Start()
        {
            _stopwatch.Start();
            _gameStateUpdatingThread.Start();
            _isRunning = true;
            Application.Run(_window);
        }

        private void WindowOnKeyUp(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.W)
            {
                _isUpKeyPressed = false;
            }
            if (args.KeyCode == Keys.S)
            {
                _isDownKeyPressed = false;
            }
            if (args.KeyCode == Keys.A)
            {
                _isLeftKeyPressed = false;
            }
            if (args.KeyCode == Keys.D)
            {
                _isRightKeyPressed = false;
            }
            if (args.KeyCode == Keys.Space)
            {
                _isShooting = false;
            }
        }

        private void GameWindowOnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.W)
            {
                _isUpKeyPressed = true;
            }
            if (args.KeyCode == Keys.S)
            {
                _isDownKeyPressed = true;
            }
            if (args.KeyCode == Keys.A)
            {
                _isLeftKeyPressed = true;
            }
            if (args.KeyCode == Keys.D)
            {
                _isRightKeyPressed = true;
            }
            if (args.KeyCode == Keys.Space)
            {
                _isShooting = true;
            }
            if (args.KeyCode == Keys.Escape)
            {
                _window.Close();
            }
        }

        private void GameWindowOnClosed(object sender, EventArgs eventArgs)
        {
            _isRunning = false;
        }

        private void GameDraw(object sender, PaintEventArgs eventArgs)
        {
            var graphics = eventArgs.Graphics;
            _ship.DrawShape(graphics);
            foreach (var bullet in _bullets)
            {
                bullet.Draw(graphics);
            }
            foreach (var asteroid in _asteroids)
            {
                asteroid.DrawShape(graphics);
            }
        }

        private void MainLoop()
        {
            while (_isRunning)
            {
                var frameStartTime = _stopwatch.ElapsedMilliseconds;
                GameUpdate();
                _canvas.Invalidate();

                if (_stopwatch.ElapsedMilliseconds - frameStartTime <= _interval)
                {
                    while ((_stopwatch.ElapsedMilliseconds - frameStartTime) < _interval) { }
                }
                else
                {
                    Console.WriteLine("Warning: Game is running slowly");
                }
            }
        }

        private void GameUpdate()
        {
            if (_isUpKeyPressed)
            {
                var movement = new PointF(0, -1).Rotate(_ship.Angle, new PointF(0, 0));
                _ship.Position = _ship.Position.Offset(movement);
            }
            if (_isDownKeyPressed)
            {
                var movement = new PointF(0, 1).Rotate(_ship.Angle, new PointF(0, 0));
                _ship.Position = _ship.Position.Offset(movement);
            }
            if (_isRightKeyPressed)
            {
                _ship.Rotate(Math.PI / 180);
            }
            if (_isLeftKeyPressed)
            {
                _ship.Rotate(-Math.PI / 180);
            }
            if (_isShooting && _ship.CanShoot(_stopwatch.ElapsedMilliseconds))
            {
                var bullet = ShipsAndAsteroidsCreator.CreateBullet(_ship);
                _bullets.Add(bullet);
            }

            foreach (var bullet in _bullets)
            {
                bullet.Move();
            }

            foreach (var asteroid in _asteroids)
            {
                var movementA = new PointF(0, -1).Rotate(asteroid.Angle, new PointF(0, 0));
                if (asteroid.Position.X > 1050)
                {
                    asteroid.Position.X = -50;
                }
                if (asteroid.Position.Y > 650)
                {
                    asteroid.Position.Y = -50;
                }
                asteroid.Position = asteroid.Position.Offset(movementA);
            }

            var asteroidsToBeRemoved = new List<ComplexShape>();
            var bulletsToBeremoved = new List<Bullet>();
            foreach (var asteroid in _asteroids)
            {
                foreach (var bullet in _bullets)
                {
                    var distanceX = asteroid.Position.X - bullet.Position.X;
                    var distanceY = asteroid.Position.Y - bullet.Position.Y;

                    var dist = distanceX * distanceX + distanceY * distanceY;
                    dist = (float)Math.Sqrt(dist);
                    if (dist < 20)
                    {
                        asteroidsToBeRemoved.Add(asteroid);
                        bulletsToBeremoved.Add(bullet);
                        break;
                    }
                }
            }

            foreach (var asteroid in asteroidsToBeRemoved)
            {
                _asteroids.Remove(asteroid);
            }
            foreach (var bullet in bulletsToBeremoved)
            {
                _bullets.Remove(bullet);
            }
        }
    }
}