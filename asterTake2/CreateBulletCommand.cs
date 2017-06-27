using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace asterTake2
{
    internal class CreateBulletCommand : IConsoleCommand
    {
        private readonly Game _game;

        public CreateBulletCommand(Game game)
        {
            _game = game;
        }

        public void DoJob(string input)
        {
            var match = Regex.Match(input, @"^\w*\s+(?:pi)(\d*)/(\d*)\s+(auto)$"); //addBullet pi3/4
            var nominator = int.Parse(match.Groups[1].Value);
            var denominator = int.Parse(match.Groups[2].Value);
            var isAutoAim = match.Groups[3].Captures.Count == 1;
            var angle = Math.PI * nominator/denominator;
            Bullet bullet;
            if (isAutoAim)
            {
                var target = _game._asteroids.First();
                bullet = new Bullet(new PointF(), angle, 10, isAutoAim, target);
            }
            else
            {
                bullet = new Bullet(new PointF(), angle, 10, isAutoAim);
            }
            _game.Bullets.Add(bullet);
        }

        public bool CanHandle(string input)
        {
            var commandNames = new[]
            {
                "addBullet",
                "bullet",
                "bulletAdd",
                "bt"
            };
            return commandNames.Any(input.StartsWith);
        }
    }
}