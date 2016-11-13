/**
 * External libraries using:
 * - MaxMind.GeoIP2
 * 
 * Sample code taken from http://maxmind.github.io/GeoIP2-dotnet/
 * 
 * 
 * Install using NuGet Package Manager Console:
 *      install-package MaxMind.GeoIP2
 *      
 * GeoIP2 city database included with this solution (GeoLite2-City.mmdb, it must placed in the build output directory)
 * 
 * Reference: https://msdn.microsoft.com/en-us/library/ms171728.aspx
 */

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
using MaxMind.GeoIP2;
using System.Threading;
using System.Collections;

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
            DataGridView.CheckForIllegalCrossThreadCalls = false;


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
        /*
        private Thread demoThread;

        // This event handler creates a thread that calls a 
        // Windows Forms control in a thread-safe way.
        private void setTextSafeBtn_Click(object sender, EventArgs e)
        {
            demoThread = new Thread(new ThreadStart(ThreadSafeAddRow));
            demoThread.Start();
        }

        // This method is executed on the worker thread and makes
        // a thread-safe call on the TextBox control.
        private void ThreadSafeAddRow()
        {
            AddRow(new String[] { "This text was set safely." });
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void AddRowCallback(String[] row);

        // This method demonstrates a pattern for making thread-safe
        // calls on a Windows Forms control. 
        //
        // If the calling thread is different from the thread that
        // created the TextBox control, this method creates a
        // SetTextCallback and calls itself asynchronously using the
        // Invoke method.
        //
        // If the calling thread is the same as the thread that created
        // the TextBox control, the Text property is set directly. 

        private void AddRow(String[] row)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (resultTable.InvokeRequired)
            {
                AddRowCallback d = new AddRowCallback(AddRow);
                this.Invoke(d, new object[] { row });
            }
            else
            {
                resultTable.Rows.Add(row);

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
            String address = "";
            if(ethernetPacket.PayloadPacket is IPv4Packet)
            {
                IPv4Packet ip = (IPv4Packet)ethernetPacket.PayloadPacket;
                address = ip.SourceAddress.ToString();
            }
            else if(ethernetPacket.PayloadPacket is IPv6Packet)
            {
                IPv6Packet ip = (IPv6Packet)ethernetPacket.PayloadPacket;
                address = ip.SourceAddress.MapToIPv4().ToString();
                Console.WriteLine("Mapping " + ip.SourceAddress.ToString() + " to " + address);
            }
            else if(ethernetPacket.PayloadPacket is ARPPacket)
            {
                ARPPacket arp = (ARPPacket)ethernetPacket.PayloadPacket;
                address = arp.SenderProtocolAddress.ToString();
                Console.WriteLine("ARP " + address);
            }
            else
            {
                return;
            }

            if (!addedAddresses.Contains(address) && !pendingAddresses.Contains(address))
            {
                pendingAddresses.Add(address);
            }
            
            /*
            if (!(ethernetPacket.PayloadPacket is IPv4Packet))
            {
                return;
            }
            IPv4Packet ipPacket = (IPv4Packet)ethernetPacket.PayloadPacket;
            int srcPort = 0, destPort = 0;
            uint seqNum = 0, ackNum = 0;
            bool validChecksum, ack = false, syn = false;
            byte[] data = { };
            ushort checksum = 0;
            if (ipPacket.PayloadPacket.GetType() == typeof(TcpPacket))
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
            } else if (ipPacket.PayloadPacket.GetType() == typeof(UdpPacket))
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

            addRow(new String[] {
                ipPacket.SourceAddress.ToString(),
                srcPort.ToString(),
                ipPacket.DestinationAddress.ToString(),
                destPort.ToString(),
                buildMAC(ethernetPacket.SourceHwAddress.GetAddressBytes()),
                buildMAC(ethernetPacket.DestinationHwAddress.GetAddressBytes()),
                ipPacket.Protocol.ToString(),
                ethernetPacket.Type.ToString()
            });
            */
        }

        private void addRow(String[] data)
        {
            /*
            demoThread = new Thread(new ThreadStart(ThreadSafeAddRow));
            demoThread.Start();
            */
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
                    timer1.Enabled = true;
                }
                else
                {
                    btnStartStop.Text = "Start";
                    device.StopCapture();
                    timer1.Enabled = false;
                }
            }
            catch (Exception)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private ArrayList pendingAddresses = new ArrayList(), addedAddresses = new ArrayList();

        private bool changed = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtCapturedData.AppendText(stringPackets);
            //stringPackets = "";
            //txtPacketCount.Text = Convert.ToString(numPackets);
            ArrayList tmp = (ArrayList) pendingAddresses.Clone();
            pendingAddresses.Clear();
            changed = false;
            foreach (String ip in tmp)
            {
                addIP(ip);
            }
            if (changed)
            {
                refreshMap();
            }
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

        private String url = "https://maps.googleapis.com/maps/api/staticmap?size=1000x400&markers=";
        private int markerCount = 0;
        private DatabaseReader reader = new DatabaseReader(@"GeoLite2-City.mmdb");

        private void addIP(String ipAddress)
        {
            if (addedAddresses.Contains(ipAddress))
            {
                return;
            }

            // Replace "City" with the appropriate method for your database, e.g.,
            // "Country".
            try
            {
                var city = reader.City(ipAddress);
                /*
                txtCapturedData.Clear();

                txtCapturedData.AppendText("City: " + city.City.Name);
                txtCapturedData.AppendText(Environment.NewLine);
                txtCapturedData.AppendText("State: " + city.MostSpecificSubdivision.Name);
                txtCapturedData.AppendText(Environment.NewLine);
                txtCapturedData.AppendText("Postal Code: " + city.Postal.Code);
                txtCapturedData.AppendText(Environment.NewLine);
                txtCapturedData.AppendText("Country: " + city.Country.Name);
                txtCapturedData.AppendText(Environment.NewLine);
                txtCapturedData.AppendText("Latitude: " + city.Location.Latitude);
                txtCapturedData.AppendText(Environment.NewLine);
                txtCapturedData.AppendText("Longitude: " + city.Location.Longitude);

                Console.WriteLine(city.Country.Name);
                Console.WriteLine(city.MostSpecificSubdivision.Name);
                Console.WriteLine(city.City.Name);
                Console.WriteLine(city.Postal.Code);
                Console.WriteLine(city.Location.Latitude);
                Console.WriteLine(city.Location.Longitude);
                */
                String coords = city.Location.Latitude + "," + city.Location.Longitude;
                if (!url.Contains(coords))
                {
                    if (markerCount == 0)
                    {
                        url += coords;
                    }
                    else
                    {
                        url += "|" + coords;
                    }
                    markerCount++;
                    changed = true;
                    string postalCode = "NA";
                    if(city.Postal.Code != null)
                    {
                        postalCode = city.Postal.Code;
                    }
                    resultTable.Rows.Add(new String[] {
                        ipAddress,
                        city.City.Name,
                        city.MostSpecificSubdivision.Name,
                        postalCode,
                        city.Country.Name,
                        city.Location.Latitude.ToString(),
                        city.Location.Longitude.ToString()
                    });
                    if (chkAutoScroll.Checked)
                    {
                        resultTable.FirstDisplayedScrollingRowIndex = resultTable.RowCount - 1;
                    }
                    // addedAddresses.Add(ipAddress);
                }

            }
            catch (MaxMind.GeoIP2.Exceptions.AddressNotFoundException)
            {
                Console.WriteLine("Could not find address " + ipAddress);
                // txtCapturedData.Text = "Address not found";
                resultTable.Rows.Add(new String[] {
                    ipAddress,
                    "NA",
                    "NA",
                    "NA",
                    "NA",
                    "NA",
                    "NA"
                });
                if (chkAutoScroll.Checked)
                {
                    resultTable.FirstDisplayedScrollingRowIndex = resultTable.RowCount - 1;
                }
                // addedAddresses.Add(ipAddress);
            }
        }

        private void refreshMap()
        {
            webBrowser.Navigate(url);
        }

        private void btnSearchIP_Click(object sender, EventArgs e)
        {
            addIP(ipAddress.Text);
        }
    }
}
