using System;
using System.Diagnostics;
using DatenChip8.Gui;

namespace DatenChip8.Core {
    /// <summary>
    /// The CPU of the DatenChip8.
    /// </summary>
    public class cpu {
        // Constants
        public const int MEMORY_SIZE = 4096;
        public const int PROGRAM_START_ADDR = 0x200;
        public const int TICK_SPEED = 8;

        // Memory of the CPU (defaults to 4096 bytes)
        private byte[] memory = new byte[MEMORY_SIZE];

        // 16 eight-bit general purpose registers
        private byte[] V = new byte[16];            
            
        // 16 bit index register
        private ushort I = 0;
            
        // Program counter (starts at 0x200)
        private ushort PC = PROGRAM_START_ADDR;

        // 16-level stack and pointer
        private Stack<ushort> stack = new Stack<ushort>();
        private ushort SP = 0;

        // Paused flag
        private Boolean paused = false;

        // Stopped flag
        private Boolean stopped = false;

        // Delay timer
        private byte DT = 0;

        // Sound timer
        private byte ST = 0;

        // Display
        private Display display;

        // Machine
        private Machine machine;

        // Keyboard
        private Input keyboard;

        // Debug flag 
        Boolean debug = false;

        // Timer stopwatch
        Stopwatch sw = new Stopwatch();

        // Awaiting Keyboard Input Flag
        bool awaitingKeyboardInput = false;

        /// <summary>
        /// CPU Constructor.
        /// </summary>
        /// <param name="display">The display that this CPU is attached to.</param>
        /// <param name="debug">Whether or not to enable debug mode.</param>
        public cpu(Display display, Input keyboard, Boolean debug, Machine machine) {
            this.display = display;
            this.keyboard = keyboard;
            this.debug = debug;
            this.machine = machine;
        }

        /// <summary>
        /// Generates a string containing the current CPU info.
        /// </summary>
        /// <returns>String containing information regarding the CPU's current state.</returns>
        private string toString() {
            string retVal = "";
            retVal += "PC: " + PC.ToString("X4") + "\n";
            retVal += "I: " + I.ToString("X4") + "\n";
            retVal += "SP: " + SP.ToString("X4") + "\n";
            retVal += "DT: " + DT.ToString("X2") + "\n";
            retVal += "ST: " + ST.ToString("X2") + "\n";
            retVal += "---------------------" + "\n";
            for (int i = 0; i < V.Length; i++) {
                retVal += "V[" + i + "]: " + V[i].ToString("X2");
                if (i < V.Length - 1) {
                    retVal += "\n";
                }                
            }

            return retVal;
        }

        /// <summary>
        /// The "heart" of the CPU. Handles fetch-decode-execute cycle.
        /// </summary>
        public void run() {
		    long cpuSpeed = 6 * Stopwatch.Frequency / 1000;
            // If the CPU is not stopped, then infinitely loop
            while (!stopped) {
                if (!sw.IsRunning || sw.ElapsedTicks > cpuSpeed) {
                    for (int i = 0; i < TICK_SPEED; i++) {
                        tick();
                    }

                    // Update timers and GUI
                    if (!paused) {
                        this.updateTimers();
                        // Invoke the display update on the GUI thread
                        this.machine.Invoke((MethodInvoker)delegate {
                            machine.updateRegisterForm(this.toString());
                        });
                    }

                    sw.Restart();
                }
                display.drawToConsole();
            }
        }

        /// <summary>
        /// Loads a ROM into the CPU at position 0x200 onwards
        /// </summary>
        /// <param name="rom">The ROM to load</param>
        public void loadRom(byte[] rom) {
            // Load ROM into memory starting from 0x200
            for (int i = 0; i < rom.Length; i++) {
                memory[PROGRAM_START_ADDR + i] = rom[i];
            }
        }

        /// <summary>
        /// Initializes the CPU by setting the registers to default values.
        /// </summary>
        public void initCpu() {
            for (int i = 0; i < V.Length; i++) {
                V[i] = 0;
            }
            // Instruction register, stack pointer, program counter, and timers all start at 0.
            I = 0;
            PC = PROGRAM_START_ADDR;
            SP = 0;
            DT = 0;
            ST = 0;

            // Paused and stopped flags should be removed
            paused = false;
            stopped = false;
        }

        /// <summary>
        /// Loads the font map into memory from 0x000.
        /// </summary>
        public void loadFont() {
            // Load font into memory starting from 0x000
            font f = new font();
            byte[] fontMap = f.getFontMap();
            for (int i = 0; i < fontMap.Length; i++) {
                memory[i] = fontMap[i];
            }
        }

        /// <summary>
        /// Decodes and executes the current instruction based on it's opcode.
        /// </summary>
        /// <param name="opcode">The opcode of the instruction to execute</param>
        public void executeInstruction(ushort opcode) {
            // Increment the program counter 
            PC += 2;

            // Get the x and y opcode nibbles (i.e. register identifiers)
            byte x = (byte)((opcode & 0x0F00) >> 8);
            byte y = (byte)((opcode & 0x00F0) >> 4);

            // Get the instruction from the opcode (i.e. mask with 0xF000)
            ushort instruction = (ushort)(opcode & 0xF000);

            if (this.debug) {
                Console.WriteLine("PC " +  PC + "; Opcode: " + opcode.ToString("X") + "; x: " + x.ToString("X") + " y: " + y.ToString("X"));
            }

            // Switch on the instruction (https://en.wikipedia.org/wiki/CHIP-8#Opcode_table)
            switch (instruction) {
                case 0x0000: // Switch on the last nibble of the opcode.
                    switch (opcode) {
                        case 0x00E0: // Clear the display
                            this.display.clearDisplayBuffer();
                            break;
                        case 0x00EE: // Returns from a subroutine. 
                            PC = stack.Pop();
                            break;
                    }
                    break;
                case 0x1000: // Jump to location nnn.
                    PC = (ushort)(opcode & 0x0FFF);
                    break;
                case 0x2000: // Calls subroutine at nnn.
                    stack.Push(PC);
                    PC = (ushort)(opcode & 0xFFF);
                    break;
                case 0x3000: // Skip next instruction if Vx = kk.
                    if (V[x] == (byte)(opcode & 0xFF)) {
                        PC += 2;
                    }
                    break;
                case 0x4000: // Skip next instruction if Vx != kk.
                    if (V[x] != (byte)(opcode & 0xFF)) {
                        PC += 2;
                    }
                    break;
                case 0x5000: // Skip next instruction if Vx = Vy.
                    if (V[x] == V[y]) {
                        PC += 2;
                    }
                    break;
                case 0x6000: // Set Vx = kk.
                    V[x] = (byte)(opcode & 0xFF);
                    break;
                case 0x7000: // Set Vx = Vx + kk.
                    V[x] += (byte)(opcode & 0xFF);
                    break;
                case 0x8000: // Switch on the last nibble of the opcode.
                    switch (opcode & 0xF) {
                        case 0x0: // Sets VX to the value of VY. 
                            V[x] = V[y];
                            break;
                        case 0x1: // Sets VX to VX or VY. (Bitwise OR operation)
                            V[x] |= V[y];
                            break;
                        case 0x2: // Sets VX to VX and VY. (Bitwise AND operation); 
                            V[x] &= V[y];
                            break;
                        case 0x3: // Sets VX to VX xor VY. (Bitwise XOR operation)
                            V[x] ^= V[y];
                            break;
                        case 0x4: // Adds VY to VX. VF is set to 1 when there's a carry, and to 0 when there is not. 
                            V[0xF] = 0;
                            if (V[x] + V[y] > 255) {
                                V[0xF] = 1;
                            } else {
                                V[0xF] = 0;
                            }
                            V[x] += V[y];
                            break;
                        case 0x5: // VY is subtracted from VX. VF is set to 0 when there's a borrow, and 1 when there is not. 
                            V[0xF] = 0;
                            if (V[x] > V[y]) {
                                V[0xF] = 1;
                            } 
                            else {
                                V[0xF] = 0;
                            }
                            V[x] -= V[y];
                            break;
                        case 0x6: // Stores the least significant bit of VX in VF and then shifts VX to the right by 1. VF is set to the value of the least significant bit of VX before the shift.
                            V[0xF] = (byte)(V[x] & 0x1);
                            V[x] >>= 1;
                            break;  
                        case 0x7: // Sets VX to VY minus VX. VF is set to 0 when there's a borrow, and 1 when there is not. 
                            V[0xF] = 0;
                            if (V[y] > V[x]) {
                                V[0xF] = 1;
                            } 
                            else {
                                V[0xF] = 0;
                            }
                            V[x] = (byte)(V[y] - V[x]);
                            break;
                        case 0xE: // Stores the most significant bit of VX in VF and then shifts VX to the left by 1.
                            V[0xF] = (byte)(V[x] & 0x80);
                            V[x] <<= 1;
                            break;
                    }
                    break;
                case 0x9000: // Skip next instruction if Vx != Vy.
                    if (V[x] != V[y]) {
                        PC += 2;
                    }
                    break;
                case 0xA000: // Set I = annn.
                    I = (ushort)(opcode & 0xFFF);
                    break;
                case 0xB000: // Jumps to the address NNN plus V0.
                    PC = (ushort)((opcode & 0xFFF) + V[0]);
                    break;
                case 0xC000: // Sets VX to the result of a bitwise and operation on a random number (Typically: 0 to 255) and NN. 
                    V[x] = (byte)((opcode & 0xFF) & (byte)(new Random().Next(0, 255)));
                    break;
                case 0xD000: // Draws a sprite at coordinate (VX, VY) that has a width of 8 pixels and a height of N pixels. 
                    int width = 8;
                    int height = (opcode & 0x000F);

                    V[0xF] = 0; // Set VF to use for use as a collision flag.
                    for (int i = 0; i < height; i++) {
                        int sprite = memory[I + i];
                        for (int j = 0; j < width; j++) {
                            if ((sprite & 0x80) > 0) {
                                if (display.drawPixel(V[x] + j, V[y] + i)) {
                                    V[0xF] = 1;
                                }
                            }
                            sprite <<= 1;
                        }
                    }

                    break;
                case 0xE000: // Switch on the last nibble of the opcode.
                    switch (opcode & 0xFF) {
                        case 0x9E: // Skips the next instruction if the key stored in VX is pressed.
                            if (keyboard.isKeyPressed(V[x])) {
                                PC += 2;
                            }
                            break;
                        case 0xA1: // Skips the next instruction if the key stored in VX is not pressed. 
                            if (!keyboard.isKeyPressed(V[x])) {
                                PC += 2;
                            }
                            break;
                    }
                    break;
                case 0xF000: // Switch on the last nibble of the opcode.
                    switch (opcode & 0xFF) {
                        case 0x07: // Sets VX to the value of the delay timer. 
                            V[x] = DT;
                            break;
                        case 0x0A: // A key press is awaited, and then stored in VX. (Blocking Operation. All instruction halted until next key event); 
                            if (this.awaitingKeyboardInput) {
                                for (int i = 0; i < 16; i++) {
                                    if (this.keyboard.isKeyPressed(i)) {
                                        V[x] = (byte)i;
                                        awaitingKeyboardInput = false;
                                        return;
                                    }
                                }
                            }
                            awaitingKeyboardInput = true;
                            PC -= 2; // Infinitely loop on this instruction until keyboad input 
                            break;
                        case 0x15: // Sets the delay timer to VX. 
                            DT = V[x];
                            break;
                        case 0x18: // Sets the sound timer to VX.
                            ST = V[x];
                            break;
                        case 0x1E: // Adds VX to I. VF is not affected. 
                            I += V[x];
                            break;
                        case 0x29: // Sets I to the location of the sprite for the character in VX.
                            I = (ushort)(V[x] * 5);
                            break;
                        case 0x33: // Stores the binary-coded decimal representation of VX, with the most significant of three digits at the address in I, the middle digit at I plus 1, and the least significant digit at I plus 2.
                            memory[I] = (byte)(V[x] / 100);
                            memory[I + 1] = (byte)((V[x] % 100) / 10);
                            memory[I + 2] = (byte)(V[x] % 10);
                            break;
                        case 0x55: // Stores from V0 to VX (including VX) in memory, starting at address I. The offset from I is increased by 1 for each value written, but I itself is left unmodified.
                            for (int i = 0; i <= x; i++) {
                                memory[I + i] = V[i];
                            }
                            break;
                        case 0x65: // Fills from V0 to VX (including VX) with values from memory, starting at address I. The offset from I is increased by 1 for each value read, but I itself is left unmodified.
                            for (int i = 0; i <= x; i++) {
                                V[i] = memory[I + i];
                            }
                            break;

                    }
                    break;
                default:
                    Console.WriteLine("Unknown opcode: " + opcode.ToString("X"));
                    break;
            }
        }

        /// <summary>
        /// Decrements the delay and sound timers if they are greater than zero.
        /// </summary>
        public void updateTimers() {
            if (DT > 0) {
                DT--;
            } else if (ST > 0) {
                ST--;
            }
        }

        /// <summary>
        /// Handles each cycle of the CPU (a "tick").
        /// </summary>
        public void tick() {
            // Only run if the CPU is not paused.
            if (!paused) {
                // Fetch the next instruction
                ushort opcode = (ushort)(memory[PC] << 8 | memory[PC + 1]);
                
                // Execute the instruction
                executeInstruction(opcode);
            }
        }

        /// <summary>
        /// Pauses the CPU.
        /// </summary>
        public void pauseCPU() {
            if (this.debug) {
                Console.WriteLine("CPU paused at PC: " + PC.ToString("X"));
            }
            this.paused = true;
        }

        /// <summary>
        /// Resumes the CPU.
        /// </summary>
        public void resumeCPU() {
            if (this.debug) {
                Console.WriteLine("CPU resumed at PC: " + PC.ToString("X"));
            }
            this.paused = false;
        }

        /// <summary>
        /// Stops the CPU, effectively killing the program.
        /// </summary>
        private void stopCPU() {
            if (this.debug) {
                Console.WriteLine("CPU stopped at PC: " + PC.ToString("X"));
            }
            this.stopped = true;
        }

        /// <summary>
        /// Clears the display and restarts the CPU.
        /// </summary>
        public void restartCPU() {
            if (this.debug) {
                Console.WriteLine("CPU restarting");
            }
            this.pauseCPU();
            this.display.clearDisplayBuffer();
            this.initCpu();
        }
    }
}