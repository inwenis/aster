﻿using System;
using System.Windows.Forms;

namespace asterTake2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var game = new Game();
            game.Start();
        }
    }
}
