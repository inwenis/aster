using System.Linq;
using System.Windows.Input;

namespace asterTake2.ConsoleCommands
{
    internal class ExitConsoleCommand : IConsoleCommand
    {
        private readonly Game _game;

        public Key ShortcutKey => Key.Q;

        public ExitConsoleCommand(Game game)
        {
            _game = game;
        }

        public void DoJob(string input)
        {
            _game.ExitConsole = true;
        }

        public bool CanHandle(string input)
        {
            var names = new[] {"exit", "quit", "x"};
            return names.Any(input.StartsWith);
        }

        public string GetHelp()
        {
            return "exit - exit console and return to game";
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }
    }
}