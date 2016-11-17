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
 * GeoIP2 city database included with this solution
 * 
 * References:
 * http://stackoverflow.com/questions/6942071/how-to-get-the-path-of-an-embebbed-resource
 */

using System;
using System.Collections;
using System.Windows.Forms;

using MaxMind.GeoIP2;
using PacketDotNet;
using SharpPcap;

namespace PacketSniff
{
    public partial class frmCapture : Form
    {
        CaptureDeviceList devices;                  // List of devices for this computer
        public ICaptureDevice device;               // Device using
        public static ICaptureDevice device2;       // Static device
        private int numPackets = 0;                 // Number of packets

        public frmCapture()
        {
            InitializeComponent();
            // DataGridView.CheckForIllegalCrossThreadCalls = false;

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

        private string getEtherType(string data)
        {
            switch (data) {
                case "0800": return "IPv4";
                case "0806": return "ARP";
                case "86DD": return "IPv6";
                    default: return "Unknown";
            }
        }

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

        private ArrayList arps = new ArrayList();
        private ArrayList gratuitousArps = new ArrayList();

        private void device_onPacketArrival(Object sender, CaptureEventArgs packet)
        {
            // Packets
            EthernetPacket ethernetPacket = (EthernetPacket) EthernetPacket.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);
            String address = null;
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
                if (arp.Operation.ToString() == "Request")
                {
                    // Limit to the last 500 ARPs
                    if (arps.Count >= 500)
                    {
                        arps.RemoveAt(0);
                    }
                    arps.Add(arp);
                } else if (arp.Operation.ToString() == "Response")
                {
                    bool found = false;
                    foreach (ARPPacket arpPacket in arps)
                    {
                        if (arp.SenderProtocolAddress.ToString() == arpPacket.TargetProtocolAddress.ToString())
                        {
                            Console.WriteLine("Found ARP Request from " + arp.SenderProtocolAddress.ToString() + "...Removing.");
                            found = true;
                            arps.Remove(arpPacket);
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("Gratuitous ARP from " + arp.SenderProtocolAddress.ToString() + " (" + buildMAC(arp.SenderHardwareAddress.GetAddressBytes()) + ")");
                        gratuitousArps.Add(arp);
                    }
                }
            }
            else
            {
                return;
            }

            // Increment packet counter
            numPackets++;

            if (!addedAddresses.Contains(address) && !pendingAddresses.Contains(address) && address != null)
            {
                pendingAddresses.Add(address);
            }
        }

        private void addRow(String[] data)
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
            txtPacketCount.Text = Convert.ToString(numPackets);
            ArrayList tmparps = (ArrayList)gratuitousArps.Clone();
            gratuitousArps.Clear();
            foreach (ARPPacket arp in tmparps)
            {
                tblGratuitousArps.Rows.Add(new String[] {
                    buildMAC(arp.SenderHardwareAddress.GetAddressBytes()),
                    arp.SenderProtocolAddress.ToString(),
                    buildMAC(arp.TargetHardwareAddress.GetAddressBytes()),
                    arp.TargetProtocolAddress.ToString()
                });
            }
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
         * Exit the application
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private String url = "https://maps.googleapis.com/maps/api/staticmap?size=800x400&markers=";
        private int markerCount = 0;
        private DatabaseReader reader = new DatabaseReader(
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PacketSniff.GeoLite2-City.mmdb")
        );

        private void addIP(String ipAddress, bool display = false)
        {
            if (addedAddresses.Contains(ipAddress) && !display)
            {
                return;
            }

            try
            {
                var city = reader.City(ipAddress);
                if (display)
                {
                    txtCapturedData.Clear();
                    txtCapturedData.AppendText("IP: " + ipAddress + Environment.NewLine);
                    txtCapturedData.AppendText("City: " + city.City.Name + Environment.NewLine);
                    txtCapturedData.AppendText("Region: " + city.MostSpecificSubdivision.Name + Environment.NewLine);
                    txtCapturedData.AppendText("Country: " + city.Country.Name + Environment.NewLine);
                    txtCapturedData.AppendText("Latitude: " + city.Location.Latitude + Environment.NewLine);
                    txtCapturedData.AppendText("Longitude: " + city.Location.Longitude + Environment.NewLine);
                }

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
                    if (display)
                    {
                        refreshMap();
                    }
                    // addedAddresses.Add(ipAddress);
                }

            }
            catch (MaxMind.GeoIP2.Exceptions.AddressNotFoundException)
            {
                if (display)
                {
                    txtCapturedData.Text = "Could not find address " + ipAddress;
                }
                Console.WriteLine("Could not find address " + ipAddress);
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
                if (chkHideLocal.Checked)
                {
                    addedAddresses.Add(ipAddress);
                }
            } catch (MaxMind.GeoIP2.Exceptions.GeoIP2Exception)
            {
                if (display)
                {
                    txtCapturedData.Text = "Could not find address " + ipAddress;
                }
                Console.WriteLine("Could not find address " + ipAddress);
            }
        }

        private void refreshMap()
        {
            webBrowser.Navigate(url);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            pendingAddresses.Clear();
            addedAddresses.Clear();
            resultTable.Rows.Clear();
            txtCapturedData.Clear();
            ipAddress.Clear();
            arps.Clear();
            gratuitousArps.Clear();
            tblGratuitousArps.Rows.Clear();
            url = "https://maps.googleapis.com/maps/api/staticmap?size=900x450&markers=";
            markerCount = 0;
            numPackets = 0;
            txtPacketCount.Text = "0";
        }

        private void btnSearchIP_Click(object sender, EventArgs e)
        {
            addIP(ipAddress.Text, true);
        }
    }
}
