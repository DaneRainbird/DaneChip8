using DatenChip8.Core;

namespace DatenChip8.Gui {
    public partial class RegistersForm : Form {
        public RegistersForm(cpu cpu) {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the textBox with info from the CPU.
        /// </summary>
        /// <param name="cpuInfo">String containing information regarding the CPU's current state.</param>
        public void updateInfo(string cpuInfo) {
            this.rchTxtBoxCpuInfo.Clear();
            this.rchTxtBoxCpuInfo.Text += cpuInfo;
        }

        /// <summary>
        /// Ensures that the form is hidden rather than closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.Hide();
        }
    }
}
