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
            var match = Regex.Match(input, @"^\w*\s*(?:pi)(\d*)/(\d*)$"); //addBullet pi3/4
            var nominator = int.Parse(match.Groups[1].Value);
            var denominator = int.Parse(match.Groups[2].Value);
            var angle = Math.PI * nominator/denominator;
            var bullet = new Bullet(new PointF(), angle, 10);
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