using System;
using DatenChip8.Core;
using DatenChip8.Gui;

namespace DatenChip8 {
    class Program
    {
        [STAThread]
        static void Main(string[] args) {
            // Create the "Machine" and run it
            Application.EnableVisualStyles();
            Machine machine = new Machine();
            Application.Run(machine);
        }
    }
}