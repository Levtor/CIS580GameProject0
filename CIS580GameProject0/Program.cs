﻿using System;

namespace CIS580GameProject0
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game0())
                game.Run();
        }
    }
}
