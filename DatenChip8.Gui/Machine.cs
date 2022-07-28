using System.Threading;
using DatenChip8.Core;
using DatenChip8.Gui;

namespace DatenChip8.Gui {
    public partial class Machine : Form {

        private cpu cpu;
        private Display display;
        private RegistersForm registersForm;
        private Input keyboard;
        private byte[] initialRom;
        private Thread cpuThread;

        /// <summary>
        /// Constructor
        /// </summary>
        public Machine() {
            InitializeComponent();

            // Initialize components and pass to new CPU
            this.display = new Display(4, 32, 64, this);
            this.keyboard = new Input();
            this.cpu = new cpu(display, keyboard, true, this);
            
            // Load initial ROM data from resources
            this.initialRom = Properties.Resources.ch8pic;

            // Load the register form
            this.registersForm = new RegistersForm(this.cpu);

            // Create the CPU Thread 
            this.cpuThread = new Thread(new ThreadStart(createCPUThread));
        }

        /// <summary>
        /// Handles creation of the CPU Thread after the main GUI has loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_Load(object sender, EventArgs e) {
            this.cpuThread.IsBackground = true;
            this.cpuThread.Start();
        }

        /// <summary>
        /// Creates the worker thread that runs the CPU of the Chip8 Emulator
        /// </summary>
        private void createCPUThread() {
            Thread.CurrentThread.Name = "CPU Thread";
            
            // Load the initial ROM (Chip8 logo) into the CPU
            cpu.loadRom(this.initialRom);
            cpu.initCpu();
            try {
                cpu.run();
            } catch (Exception e) {
                MessageBox.Show(e.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine(e.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// Handles the pause/play button in the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPausePlay_Click(object sender, EventArgs e) {
            Button button = (Button)sender;
            if (button.Text == "Pause") {
                button.Text = "Play";
                this.Text += " (Paused)";
                this.cpu.pauseCPU();
            } else {
                button.Text = "Pause";
                this.Text = this.Text.Replace(" (Paused)", "");
                this.cpu.resumeCPU();
            }
        }

        /// <summary>
        /// Opens the CPU Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowCpuInfoForm_Click(object sender, EventArgs e) {
            this.registersForm.Show();
        }

        /// <summary>
        /// Handles updating the CPU Info form with the CPU details passed from the worker thread.
        /// </summary>
        /// <param name="cpuDetails">String containing information regarding the CPU's current state.</param>
        public void updateRegisterForm(string cpuDetails) {
            this.registersForm.updateInfo(cpuDetails);
        }

        /// <summary>
        /// Handles the restart button click event and restarts the CPU
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e) {
            handleCPURestart();
        }

        /// <summary>
        /// Restarts the CPU and updates the UI to match the current "playing" state
        /// </summary>
        private void handleCPURestart() {
            this.btnPausePlay.Text = "Pause";
            this.Text = this.Text.Replace(" (Paused)", "");
            this.cpu.restartCPU();
        }

        /// <summary>
        /// Handles the "Select ROM" option in the menu bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectROMToolStripMenuItem_Click(object sender, EventArgs e) {
            // Pause the CPU while the file picker opens
            this.cpu.pauseCPU();

            // Create a new file dialog
            OpenFileDialog ofd = new OpenFileDialog() {
                FileName = "Select a CHIP8 ROM",
                Title = "Open CHIP8 ROM"
            };
            // If the file dialog has selected a file, then try load that file, otherwise resume operation
            if (ofd.ShowDialog() == DialogResult.OK) {
                string filePath = ofd.FileName;
                this.cpu.loadRom(System.IO.File.ReadAllBytes(filePath));
                handleCPURestart();
            } else {
                this.cpu.resumeCPU();
            }
        }

        /// <summary>
        /// Handles KeyDown events on the form and passes to the Input module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_KeyDown(object sender, KeyEventArgs e) {
            this.keyboard.setKeyPressed(e.KeyCode);
        }

        /// <summary>
        /// Handles KeyUp events on the form and passes to the Input module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_KeyUp(object sender, KeyEventArgs e) {
            this.keyboard.setKeyReleased(e.KeyCode);
        }

        /// <summary>
        /// Handles closing the form and gracefully exiting the CPU thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_FormClosing(object sender, FormClosingEventArgs e) {
            // Pause the CPU to prevent error messages popping up
            this.cpu.pauseCPU();

            // Exit the application and gracefully close the CPU thread
            Environment.Exit(Environment.ExitCode);
        }
    }
}
