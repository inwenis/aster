using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    class CreateAutoAimBulletOnKeyB : IConsoleCommand
    {
        private readonly CreateBulletCommand _createBulletCommand;

        public Key ShortcutKey => Key.A;

        public CreateAutoAimBulletOnKeyB(CreateBulletCommand createBulletCommand)
        {
            _createBulletCommand = createBulletCommand;
        }

        public void DoJob(string input)
        {
            _createBulletCommand.DoJob("bullet pi3/4 auto");
        }

        public bool CanHandle(string input)
        {
            return input == "b";
        }

        public string GetHelp()
        {
            return "b - bullet pi3/4 auto";
        }

        public void DoJobOnShortcutKey()
        {
            throw new System.NotImplementedException();
        }
    }
}