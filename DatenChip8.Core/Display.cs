using System;
using System.IO;

namespace DatenChip8.Core {
    public class Display {
        // Constants
        public const int WIDTH = 64;
        public const int HEIGHT = 32;
        
        // Adjustable values (!TODO make these configurable)
        private int scale { get; set; }
        private int width { get; set; } 
        private int height { get; set; }

        // Display buffer
        private byte[] displayBuffer;  

        public Display(int scale, int width, int height) {
            // Initialize display
            this.displayBuffer = new byte[WIDTH * HEIGHT];
        }

        public bool drawPixel(int x, int y) {
            // If pixel is out of bounds then wrap to the other side of the screen
            if (x < 0) {
                x = WIDTH + x;
            } else if (x >= WIDTH) {
                x = x - WIDTH;
            }

            // If the pixel is out of bounds thne wrap to the the other side of the screen
            if (y < 0) {
                y = HEIGHT + y;
            } else if (y >= HEIGHT) {
                y = y - HEIGHT;
            }

            // Get the pixel index
            int index = x + (y * WIDTH);

            // Pixels are XORed into the screen
            this.displayBuffer[index] ^= 1;

            // Return true if the pixel was drawn 

            return this.displayBuffer[index] == 0;
        }

        public void clearDisplayBuffer() {
            // Clear the display buffer
            for (int i = 0; i < this.displayBuffer.Length; i++) {
                this.displayBuffer[i] = 0;
            }
        }

        public void drawToConsole() {
            // Clear the console screen
            Console.Clear();

            // Draw the display buffer to the console
            for (int y = 0; y < HEIGHT; y++) {
                for (int x = 0; x < WIDTH; x++) {
                    if (this.displayBuffer[x + (y * WIDTH)] == 1) {
                        Console.Write("██");
                    } else {
                        Console.Write("░░");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}