using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacketSniff
{
    public partial class frmSend : Form
    {
        public static int instantiations = 0;

        public frmSend()
        {
            InitializeComponent();
            instantiations++;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the dialog
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            openFileDialog1.Title = "Open captured packets";
            openFileDialog1.ShowDialog();

            // Verify a file name was specified
            if (openFileDialog1.FileName != "")
            {
                txtPacket.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the dialog
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save the captured packets";
            saveFileDialog1.ShowDialog();

            // Verify a file name was specified
            if (saveFileDialog1.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, txtPacket.Text);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string strBytes = "";

            // Get hex values
            foreach (string s in txtPacket.Lines)
            {
                // Removing comments
                string[] noComments = s.Split('#');
                string s1 = noComments[0];
                strBytes += s1 + Environment.NewLine;
            }

            // Extract hex values into byte array
            string[] sBytes = strBytes.Split(new string[] {"\n", "\r\n", " ", "\r", "\t"}, StringSplitOptions.RemoveEmptyEntries);

            // Change strings to bytes
            byte[] packet = new byte[sBytes.Length];
            int i = 0;
            foreach (string s in sBytes)
            {
                packet[i] = Convert.ToByte(s, 16);
                i++;
            }

            // Send the packet
            try
            {
                frmCapture.device.SendPacket(packet);
            }
            catch (Exception ex)
            {
                // Do nothing
                Console.WriteLine(ex.Message);
            }
        }

        private void frmSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            instantiations--;
        }
    }
}
