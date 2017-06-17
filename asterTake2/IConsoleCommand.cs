namespace asterTake2
{
    internal interface IConsoleCommand
    {
        void DoJob(string input);
        bool CanHandle(string input);
    }
}