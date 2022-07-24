using System.Threading;
using DatenChip8.Core;
using DatenChip8.Gui;

namespace DatenChip8.Gui {
    public partial class Machine : Form {

        private cpu cpu;
        private Display display;
        private RegistersForm registersForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public Machine() {
            InitializeComponent();

            // Initialize components and pass to new CPU
            this.display = new Display(4, 32, 64, this);
            this.cpu = new cpu(display, true, this);

            // Load the register form
            this.registersForm = new RegistersForm(this.cpu);
        }

        /// <summary>
        /// Handles creation of the CPU Thread after the main GUI has loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Machine_Load(object sender, EventArgs e) {
            new Thread(() => 
                {
                    createCPUThread();
                }).Start();
        }

        private void createCPUThread() {
            Thread.CurrentThread.Name = "CPU Thread";

            // Read ROM file from roms/test_opcode.ch8
            // !TODO - create an option to select a ROM file to load
            byte[] rom = System.IO.File.ReadAllBytes("roms/test_opcode.ch8");
            cpu.loadRom(rom);
            cpu.initCpu();
            try {
                cpu.run();
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
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
        /// Handles restarting the CPU.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e) {
            this.btnPausePlay.Text = "Pause";
            this.Text = this.Text.Replace(" (Paused)", "");
            this.cpu.restartCPU();
        }
    }
}
