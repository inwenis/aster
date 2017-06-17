namespace asterTake2
{
    internal class ExitCommand : IConsoleCommand
    {
        private readonly Game _game;

        public ExitCommand(Game game)
        {
            _game = game;
        }

        public void DoJob()
        {
            _game._exitConsole = true;
        }

        public bool CanHandle(string[] commandName)
        {
            return commandName[0] == "exit";
        }
    }
}