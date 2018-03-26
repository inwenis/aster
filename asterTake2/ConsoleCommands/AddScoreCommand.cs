using System.Windows;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    internal class AddScoreCommand : IConsoleCommand
    {
        private Game _game;

        public AddScoreCommand(Game game)
        {
            _game = game;
        }

        public Key ShortcutKey => Key.S;

        public bool CanHandle(string input)
        {
            return input == "addScore";
        }

        public void DoJob(string input)
        {
            _game.Score.Points += 1000;
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "addScore - add 1000 score points";
        }
    }
}