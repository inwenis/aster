﻿using System.Windows.Input;

namespace asterTake2.ConsoleCommands
{
    internal interface IConsoleCommand
    {
        void DoJob(string input);
        bool CanHandle(string input);
        string GetHelp();
        Key ShortcutKey { get; }

        void DoJobOnShortcutKey();
    }
}