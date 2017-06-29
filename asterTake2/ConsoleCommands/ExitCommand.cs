using System.Linq;

namespace asterTake2.ConsoleCommands
{
    internal class ExitCommand : IConsoleCommand
    {
        private readonly Game _game;

        public ExitCommand(Game game)
        {
            _game = game;
        }

        public void DoJob(string input)
        {
            _game._exitConsole = true;
        }

        public bool CanHandle(string input)
        {
            var names = new[] {"exit", "quit", "x"};
            return names.Any(input.StartsWith);
        }
    }
}