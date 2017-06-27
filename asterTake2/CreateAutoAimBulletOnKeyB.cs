namespace asterTake2
{
    class CreateAutoAimBulletOnKeyB : IConsoleCommand
    {
        private readonly CreateBulletCommand _createBulletCommand;

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
    }
}