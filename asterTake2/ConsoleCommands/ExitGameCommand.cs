using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    internal class ExitGameCommand : IConsoleCommand
    {
        private Game _game;

        public ExitGameCommand(Game game)
        {
            _game = game;
        }

        public Key ShortcutKey => Key.Q;

        public bool CanHandle(string input)
        {
            return input == "gameExit";
        }

        public void DoJob(string input)
        {
            _game.IsRunning = false;
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "gameExit - exit the game";
        }
    }
}