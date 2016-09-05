using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;

namespace PacketSniff
{
    public partial class Form1 : Form
    {
        CaptureDeviceList devices;              // List of devices for this computer
        public static ICaptureDevice device;    // Device using
        public static string stringPackets = "";       // Captured data

        public Form1()
        {
            InitializeComponent();

            // Get all the machine's devices
            devices = CaptureDeviceList.Instance;

            // Exit if there aren't any devices
            if (devices.Count < 1)
            {
                MessageBox.Show("No Capture Devices Found");
                Application.Exit();
            }

            // Populate the combo box
            foreach (ICaptureDevice dev in devices)
            {
                cmbDevices.Items.Add(dev.Description);
            }

            // Get the second device and display it in the combo box
            setDevice(devices[2]);
        }

        private static void device_onPacketArrival(Object sender, CaptureEventArgs packet)
        {
            // array to store data
            byte[] data = packet.Packet.Data;

            // track bytes displayed per line
            int byteCounter = 0;

            // process each byte in the captured packet
            foreach (byte b in data)
            {
                // Add byte to the string in hexadecimal
                stringPackets += b.ToString("X2") + " ";
                byteCounter++;
                if (byteCounter == 16)
                {
                    byteCounter = 0;
                    stringPackets += Environment.NewLine;
                }
            }
            stringPackets += Environment.NewLine;
            stringPackets += Environment.NewLine;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStartStop.Text == "Start")
                {
                    btnStartStop.Text = "Stop";
                    device.StartCapture();
                    timer1.Enabled = true;
                }
                else
                {
                    btnStartStop.Text = "Start";
                    device.StopCapture();
                    timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.GetType();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtCapturedData.AppendText(stringPackets);
            stringPackets = "";
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDevice(devices[cmbDevices.SelectedIndex]);
        }

        private void setDevice(ICaptureDevice dev)
        {
            device = dev;
            cmbDevices.Text = device.Description;

            // Register handler function to the 'packet arrival' event
            device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_onPacketArrival);

            // Open device for capturing
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
        }
    }
}
