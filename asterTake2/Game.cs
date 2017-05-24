using System;
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

        private bool _isUpKeyPressed;
        private bool _isRightKeyPressed;
        private bool _isDownKeyPressed;
        private bool _isLeftKeyPressed;

        public Game()
        {
            _window = new Form
            {
                FormBorderStyle = FormBorderStyle.Fixed3D, //what does this mean?
                ClientSize = new Size(600, 800),
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
        }

        private void GameWindowOnClosed(object sender, EventArgs eventArgs)
        {
            _isRunning = false;
        }

        private void GameDraw(object sender, PaintEventArgs eventArgs)
        {
            var graphics = eventArgs.Graphics;
            _ship.DrawShape(graphics);
            foreach (var complexShape in _asteroids)
            {
                complexShape.DrawShape(graphics);
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

            foreach (var complexShape in _asteroids)
            {
                var movementA = new PointF(0, -1).Rotate(complexShape.Angle, new PointF(0, 0));
                complexShape.Position = complexShape.Position.Offset(movementA);
            }
        }
    }
}