namespace asterTake2
{
    internal interface IConsoleCommand
    {
        void DoJob();
        bool CanHandle(string[] commandName);
    }
}