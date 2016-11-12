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
    public partial class frmCapture : Form
    {
        CaptureDeviceList devices;                  // List of devices for this computer
        public ICaptureDevice device;               // Device using
        public static ICaptureDevice device2;       // Static device
        // private string stringPackets = "";       // Captured data
        // private int numPackets = 0;              // Number of packets
        frmSend fSend;                              // Send form

        public frmCapture()
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

            // Get the first device and display it in the combo box
            if (devices.Count > 0)
            {
                setDevice(devices[0]);
            }
        }

        /*
        private string getEtherType(string data)
        {
            switch (data) {
                case "0800": return "IPv4";
                case "0806": return "ARP";
                case "86DD": return "IPv6";
                    default: return "Unknown";
            }
        }
        */

        private String buildMAC(byte[] bytes)
        {
            String output = "";
            int counter = 1;
            foreach (byte b in bytes)
            {
                output += b.ToString("X2");
                if (counter != bytes.Length)
                {
                    output += ":";
                }
                counter++;
            }
            return output;
        }

        private void device_onPacketArrival(Object sender, CaptureEventArgs packet)
        {
            /*
            // Increment packet counter
            numPackets++;

            // Prefix packet with the packet number in the capture window
            stringPackets += "Packet Number: " + Convert.ToString(numPackets);
            stringPackets += Environment.NewLine;
            */

            // Packets
            EthernetPacket ethernetPacket = (EthernetPacket) EthernetPacket.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);
            //IPv6Packet ipv6 = (IPv6Packet)ethernetPacket.PayloadPacket;
            //IPv4Packet ipv4 = (IPv4Packet)ethernetPacket.PayloadPacket;
            if (!(ethernetPacket.PayloadPacket is IpPacket))
            {
                return;
            }
            IpPacket ipPacket = (IpPacket)ethernetPacket.PayloadPacket;
            /*
            if (!(ethernetPacket.PayloadPacket is IPv4Packet))
            {
                return;
            }
            IPv4Packet ipPacket = (IPv4Packet)ethernetPacket.PayloadPacket;
            */
            int srcPort, destPort;
            uint seqNum = 0, ackNum = 0;
            bool validChecksum, ack = false, syn = false;
            byte[] data;
            ushort checksum;
            if (ipPacket.PayloadPacket is TcpPacket)
            {
                TcpPacket tcp = (TcpPacket)ipPacket.PayloadPacket;
                srcPort = tcp.SourcePort;
                destPort = tcp.DestinationPort;
                validChecksum = tcp.ValidChecksum;
                checksum = tcp.Checksum;
                data = tcp.PayloadData;
                ack = tcp.Ack;
                syn = tcp.Syn;
                seqNum = tcp.SequenceNumber;
                ackNum = tcp.AcknowledgmentNumber;
            } else if (ipPacket.PayloadPacket is UdpPacket)
            {
                UdpPacket udp = (UdpPacket)ipPacket.PayloadPacket;
                srcPort = udp.SourcePort;
                destPort = udp.DestinationPort;
                checksum = udp.Checksum;
                validChecksum = udp.ValidChecksum;
                data = udp.PayloadData;
            } else
            {
                return;
            }

            //if (destPort == 80 || srcPort == 80)
            //{
                addRow(new object[] {
                    ipPacket.SourceAddress.ToString(),
                    srcPort,
                    ipPacket.DestinationAddress.ToString(),
                    destPort,
                    buildMAC(ethernetPacket.SourceHwAddress.GetAddressBytes()),
                    buildMAC(ethernetPacket.DestinationHwAddress.GetAddressBytes()),
                    ipPacket.Protocol,
                    ethernetPacket.Type.ToString(),
                    ipPacket.TimeToLive,
                    ipPacket.HeaderLength,
                    ipPacket.TotalLength,
                    ack,
                    syn,
                    seqNum,
                    ackNum,
                    checksum,
                    validChecksum,
                    System.Text.Encoding.ASCII.GetString(data)
                });
            //}
            /*
            // Reset byte counter
            byteCounter = 0;

            // Add raw data header
            stringPackets += "Raw Data:" + Environment.NewLine;

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
            */
        }

        private void addRow(object[] data)
        {
            resultTable.Rows.Add(data);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStartStop.Text == "Start")
                {
                    btnStartStop.Text = "Stop";
                    device.StartCapture();
                    //timer1.Enabled = true;
                }
                else
                {
                    btnStartStop.Text = "Start";
                    device.StopCapture();
                    //timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtCapturedData.AppendText(stringPackets);
            //stringPackets = "";
            //txtPacketCount.Text = Convert.ToString(numPackets);
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDevice(devices[cmbDevices.SelectedIndex]);
        }

        private void setDevice(ICaptureDevice dev)
        {
            device = dev;
            cmbDevices.Text = device.Description;
            txtGUID.Text = device.Name;

            // Register handler function to the 'packet arrival' event
            device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_onPacketArrival);

            // Open device for capturing
            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            device2 = device;
        }

        /**
         * Save captured packets to a file
         */
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the dialog
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save the captured packets";
            saveFileDialog1.ShowDialog();

            // Verify a file name was specified
            if (saveFileDialog1.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, txtCapturedData.Text);
            }
        }

        /**
         * Exit the application
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /**
         * Open captured packets in a file
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the dialog
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            openFileDialog1.Title = "Open captured packets";
            openFileDialog1.ShowDialog();

            // Verify a file name was specified
            if (openFileDialog1.FileName != "")
            {
                txtCapturedData.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void sendWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSend.instantiations == 0)
            {
                fSend = new frmSend(); // Creates a new frmSend
                fSend.Show();
            }
        }

        private void resultTable_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
