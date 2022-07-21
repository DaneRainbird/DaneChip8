using System;
using DatenChip8.Core;

namespace DatenChip8 {
    class Program
    {
        static void Main(string[] args) {
            // Initialize components and pass to new CPU
            Display display = new Display(4, 32, 64);

            cpu cpu = new cpu(display);
            
            // Read ROM file from roms/test_opcode.ch8
            byte[] rom = System.IO.File.ReadAllBytes("roms/test_opcode.ch8");
            cpu.loadRom(rom);
            cpu.initCpu();
            try { 
                cpu.run();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}