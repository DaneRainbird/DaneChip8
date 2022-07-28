using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DatenChip8.Gui;

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

        // The "Machine" that owns this display
        private Machine machine;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="machine">The machine that owns this display</param>
        /// <param name="scale">The scale of the display</param>
        /// <param name="width">The width of the display</param>
        /// <param name="height">The height of the display</param>
        public Display(int scale, int width, int height, Machine machine) {
            // Initialize display
            this.displayBuffer = new byte[WIDTH * HEIGHT];
            this.machine = machine;
        }

        /// <summary>
        /// Sets a pixel to be "drawn" (i.e. enabled or disabled) in the displayBuffer
        /// </summary>
        /// <param name="x">The x coordinate of the pixel</param>
        /// <param name="y">The y coordinate of the pixel</param>
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

        /// <summary>
        /// Clears the display buffer
        /// </summary>
        public void clearDisplayBuffer() {
            // Clear the display buffer
            for (int i = 0; i < this.displayBuffer.Length; i++) {
                this.displayBuffer[i] = 0;
            }
        }

        /// <summary>
        /// Draws the display buffer to the GUI
        /// </summary>
        public void drawToConsole() {
            Bitmap initalBitmap = new Bitmap(WIDTH, HEIGHT);

            // Create a bitmap image to be displayed on the WinForm
            for (int y = 0; y < HEIGHT; y++) {
                for (int x = 0; x < WIDTH; x++) {
                    if (this.displayBuffer[x + (y * WIDTH)] == 1) {
                        initalBitmap.SetPixel(x, y, this.machine.getDisplayColours()[1]);
                    } else {
                        initalBitmap.SetPixel(x, y, this.machine.getDisplayColours()[0]);
                    }
                }
            }

           // Create a container for the bitmap image to allow for resizing
            Rectangle outputContainerRect = new Rectangle(0, 0, WIDTH * 10, HEIGHT * 10);
            Bitmap outputBitmap = new Bitmap(WIDTH * 10, HEIGHT * 10);

            outputBitmap.SetResolution(initalBitmap.HorizontalResolution, initalBitmap.VerticalResolution);

            // Create a graphics object to draw the bitmap image to the container
            using (Graphics graphics = Graphics.FromImage(outputBitmap)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(initalBitmap, outputContainerRect, 0, 0, initalBitmap.Width, initalBitmap.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            // Invoke the display update on the GUI thread
            machine.Invoke((MethodInvoker)delegate {
                machine.picBoxGameDisplay.Image = outputBitmap;
            });
        }
    }
}