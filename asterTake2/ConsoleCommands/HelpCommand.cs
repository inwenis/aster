using System;
using System.Collections.Generic;
using System.Windows.Input;
using asterTake2.ConsoleCommands;

namespace asterTake2
{
    internal class HelpCommand : IConsoleCommand
    {
        public List<IConsoleCommand> Commands { get; internal set; }

        public Key ShortcutKey => Key.H;

        public bool CanHandle(string input)
        {
            return input == "help";
        }

        public void DoJob(string input)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            foreach (var consoleCommand in Commands)
            {
                Console.WriteLine(consoleCommand.ShortcutKey + " " + consoleCommand.GetHelp());
            }
        }

        public void DoJobOnShortcutKey()
        {
            DoJob("");
        }

        public string GetHelp()
        {
            return "help - print help";
        }
    }
}