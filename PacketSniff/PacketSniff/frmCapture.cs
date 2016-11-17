/**
 * Michael Palmer
 * Computer Security Project
 * Fall 2016
 * PacketSniff MultiTool
 * 
 * External libraries:
 * - MaxMind.GeoIP2
 * - Sharpcap
 * 
 * References:
 * - GeoIP2 (database is packaged with this solution)
 * - Sample code for GeoIP lookups taken from http://maxmind.github.io/GeoIP2-dotnet/
 * - Embedded resources: http://stackoverflow.com/questions/6942071/how-to-get-the-path-of-an-embebbed-resource
 * - Google Static Maps API
 * 
 * Installation:
 * This application uses MaxMind's GeoIP to perform IP lookups. The package can be installed
 * very easily via the NuGet Package Manager (Tools -> NuGet Package Manager).
 * Select "Manage NuGet Packages for Solution" and Visual Studio should prompt you to install the required
 * packages. If not, the package can be installed manually by running the following command in the NuGet Package Manager Console: 
 *      install-package MaxMind.GeoIP2
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
        private CaptureDeviceList devices;          // List of devices for this computer
        private ICaptureDevice device;              // Device using
        private int numPackets = 0;                 // Number of packets

        // Arrays to track ARP packets and addresses
        private ArrayList arpRequests = new ArrayList();
        private ArrayList gratuitousArps = new ArrayList();
        private Hashtable pendingAddresses = new Hashtable();
        private ArrayList addedAddresses = new ArrayList();

        // Variables for the map
        private bool doMapRefresh = false;
        private String mapUrl = "https://maps.googleapis.com/maps/api/staticmap?size=350x250&markers=";
        private int markerCount = 0;

        // GeoIP database reader
        private DatabaseReader reader = new DatabaseReader(
            System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PacketSniff.GeoLite2-City.mmdb")
        );

        /**
         * Constructor
         */
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

        /**
         * Format a set of address bytes into a human-readable MAC address
         * 
         * param name="bytes" byte array of address bytes
         * returns string
         */
        private String buildMAC(byte[] bytes)
        {
            String output = "";
            int counter = 1;
            foreach (byte b in bytes)
            {
                // Convert to hex
                output += b.ToString("X2");

                // Add separators if we aren't at the last byte
                if (counter != bytes.Length)
                {
                    output += ":";
                }
                counter++;
            }
            return output;
        }

        /**
         * Process packets when they arrive
         */
        private void device_onPacketArrival(Object sender, CaptureEventArgs packet)
        {
            try
            {
                String address = null, protocol = null;

                // Convert the packet data from a byte array to a EthernetPacket instance
                EthernetPacket ethernetPacket = (EthernetPacket)Packet.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);
                
                // IPv4
                if (ethernetPacket.PayloadPacket is IPv4Packet)
                {
                    IPv4Packet ip = (IPv4Packet)ethernetPacket.PayloadPacket;
                    address = ip.SourceAddress.ToString();
                    protocol = ip.Protocol.ToString();
                }

                // IPv6
                else if (ethernetPacket.PayloadPacket is IPv6Packet)
                {
                    IPv6Packet ip = (IPv6Packet)ethernetPacket.PayloadPacket;

                    // Convert address to IPv4 since the GeoIP database we're using doesn't support IPv6 lookups
                    address = ip.SourceAddress.MapToIPv4().ToString();
                    protocol = ip.Protocol.ToString();
                }

                // ARP
                else if (ethernetPacket.PayloadPacket is ARPPacket)
                {
                    ARPPacket arp = (ARPPacket)ethernetPacket.PayloadPacket;
                    address = arp.SenderProtocolAddress.ToString();

                    // Check if this is a gratuitious ARP
                    checkForGratuitiousArp(arp);
                    protocol = "ARP";
                }

                // Other
                else
                {
                    // Don't care about other packet types
                    return;
                }

                

                // We care about this packet, so the packet counter can increment
                numPackets++;

                // If we haven't processes this IP address yet, add it to the address queue
                if (address != null && !addedAddresses.Contains(address) && !pendingAddresses.ContainsKey(address))
                {
                    pendingAddresses.Add(address, protocol);
                }
            } catch (Exception)
            {
                // Handle issues gracefully
            }
        }

        /**
         * Check if this ARP packet is gratuitious
         * 
         * Each ARP request that is received is saved to an ArrayList. Every time an ARP response goes out,
         * it will check if there is a corresponding ARP request. If there is, the ARP request is removed from
         * the ArrayList. If not, then a gratuitous ARP has been discovered, so it is added to a separate ArrayList
         * containing all the gratutitous ARPs that have been received. When the timer runs, it will be added to the table.
         */
        private void checkForGratuitiousArp(ARPPacket arp)
        {
            // ARP Request
            if (arp.Operation.ToString() == "Request")
            {
                // Limit to the last 500 ARPs (just in case memory goes crazy)
                if (arpRequests.Count >= 500)
                {
                    // Remove the oldest one
                    arpRequests.RemoveAt(0);
                }

                // Add the ARP request to the ArrayList so we can check against it later
                arpRequests.Add(arp.TargetProtocolAddress.ToString());
            }

            // ARP Response
            else if (arp.Operation.ToString() == "Response")
            {
                // Use this to track if a corresponding ARP request has been found
                bool found = false;

                foreach (String targetAddress in arpRequests)
                {
                    if (arp.SenderProtocolAddress.ToString() == targetAddress)
                    {
                        // Found a corresponding ARP request, which means it is safe to remove it from the ARP request ArrayList
                        found = true;
                        arpRequests.Remove(targetAddress);
                        break;
                    }
                }

                // If found is still false, then this is a gratuitous ARP
                if (!found)
                {
                    gratuitousArps.Add(arp);
                }
            }
        }

        /**
         * Start/Stop Packet Capturing
         */
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
                // If something happens, just let it pass through. Any exceptions that get raised here
                // tend to be related to the timer, which will not break the program.
            }
        }
        
        /**
         * Perform operations when the timer ticks
         */
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Update the packet counter textbox
                txtPacketCount.Text = Convert.ToString(numPackets);

                /** GRATUITIOUS ARPS **/

                // Make a copy of the gratuitous ARPs so we can keep collecting while we add to the table
                ArrayList tmpArps = (ArrayList)gratuitousArps.Clone();

                // It's safe to clear this now
                gratuitousArps.Clear();

                // Loop through the gratuitious ARPs and add them to the table
                foreach (ARPPacket arp in tmpArps)
                {
                    tblGratuitousArps.Rows.Add(new String[] {
                        buildMAC(arp.SenderHardwareAddress.GetAddressBytes()),
                        arp.SenderProtocolAddress.ToString(),
                        buildMAC(arp.TargetHardwareAddress.GetAddressBytes()),
                        arp.TargetProtocolAddress.ToString()
                    });
                }

                // If auto scroll is enabled, then scroll the row.
                if (chkAutoScroll.Checked && tblGratuitousArps.RowCount > 0)
                {
                    tblGratuitousArps.FirstDisplayedScrollingRowIndex = tblGratuitousArps.RowCount - 1;
                }

                /** PACKET LOCATIONS **/

                // Make a copy of the pending ip addresses so we can keep collecting while we add to the table
                Hashtable tmpAddresses = (Hashtable)pendingAddresses.Clone();

                // Safe to clear this now
                pendingAddresses.Clear();

                // Reset the doMapRefresh to false before we start processing
                doMapRefresh = false;

                // Loop through the pending ip addresses and add them to the table and map
                foreach (DictionaryEntry entry in tmpAddresses)
                {
                    addIP(entry.Key.ToString(), entry.Value.ToString());
                }

                // Check if the map needs to be refreshed
                if (doMapRefresh)
                {
                    refreshMap();
                }
            } catch (Exception)
            {
                // Let it pass
            }
        }

        /**
         * Update the capture device when a different one is selected
         */
        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDevice(devices[cmbDevices.SelectedIndex]);
        }

        /**
         * Set the capture device
         */
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
        }

        /**
         * Exit the application
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /**
         * Add an IP address to the table and map
         * 
         * param name="ipAddress" IP address to add
         * param name="protocol" Protocol
         * param name="manual" If true, this is a manual lookup and the GeoIP information should be shown in the textbook. Defaults to false.
         */
        private void addIP(String ipAddress, String protocol = "", bool manual = false)
        {
            // Exit if this is not a manual lookup and the address has already been processed. We don't need duplicates.
            if (addedAddresses.Contains(ipAddress) && !manual)
            {
                return;
            }

            try
            {
                // Peform the lookup with GeoIP
                var city = reader.City(ipAddress);

                // If this is a manual lookup, output the information to the textbox
                if (manual)
                {
                    txtResults.Clear();
                    txtResults.AppendText("IP: " + ipAddress + Environment.NewLine);
                    txtResults.AppendText("City: " + city.City.Name + Environment.NewLine);
                    txtResults.AppendText("Region: " + city.MostSpecificSubdivision.Name + Environment.NewLine);
                    txtResults.AppendText("Country: " + city.Country.Name + Environment.NewLine);
                    txtResults.AppendText("Latitude: " + city.Location.Latitude + Environment.NewLine);
                    txtResults.AppendText("Longitude: " + city.Location.Longitude + Environment.NewLine);
                }

                // Build the map coordinates
                String coords = city.Location.Latitude + "," + city.Location.Longitude;

                // If the map already has these coordinates, it doesn't make sense to add another marker right on
                // top of the existing one. Plus, Google limits the URL to a certain length.
                if (!mapUrl.Contains(coords))
                {
                    // If this is the first market, just append it to the URL, otherwise, add a delimiter.
                    mapUrl += ((markerCount == 0) ? "" : "|") + coords;

                    // Increment the marker count
                    markerCount++;

                    // New coordinates have been added to the url, so we need to let the timer know to refresh the map
                    doMapRefresh = true;

                    // Postal code could be null if the address is not US-based, so we need to have a default value
                    // for other countries to use.
                    string postalCode = (city.Postal.Code != null) ? city.Postal.Code : "NA";

                    // Add the row to the result table
                    resultTable.Rows.Add(new String[] {
                        ipAddress,
                        protocol,
                        city.City.Name,
                        city.MostSpecificSubdivision.Name,
                        postalCode,
                        city.Country.Name,
                        city.Location.Latitude.ToString(),
                        city.Location.Longitude.ToString()
                    });

                    // If this was a manual lookup, the map should be refreshed immediately
                    if (manual)
                    {
                        refreshMap();
                    }
                }

            }
            catch (MaxMind.GeoIP2.Exceptions.AddressNotFoundException)
            {
                // GeoIP is great, but it doesn't have everything (i.e. private addresses, of course). Handling those situations cleanly here.

                // If this was a manual lookup, put an error message in the textbox
                if (manual)
                {
                    txtResults.Text = "Could not find address " + ipAddress;
                }
                else
                {
                    // Not a manual lookup, so let's put a row for that IP in the table
                    resultTable.Rows.Add(new String[] { ipAddress, protocol });
                }

                // If the Hide Local checkbox is selected, add this address (most likely a private address) to the array so we don't report on it
                // in future packets. The main purpose of this is just to reduce clutter in the table.
                if (chkHideLocal.Checked)
                {
                    addedAddresses.Add(ipAddress);
                }
            } catch (MaxMind.GeoIP2.Exceptions.GeoIP2Exception)
            {
                // Catch all other GeoIP exceptions gracefully
                // If this was a manual lookup, put an error message in the textbox
                if (manual)
                {
                    txtResults.Text = "Could not find address " + ipAddress;
                }
                else
                {
                    // Not a manual lookup, so let's put a row for that IP in the table
                    resultTable.Rows.Add(new String[] { ipAddress, protocol });
                }
            }

            // If auto scroll is enabled, then scroll the row.
            if (chkAutoScroll.Checked && resultTable.RowCount > 0)
            {
                resultTable.FirstDisplayedScrollingRowIndex = resultTable.RowCount - 1;
            }
        }

        /**
         * Perform a map refresh
         */
        private void refreshMap()
        {
            webBrowser.Navigate(mapUrl);
        }

        /**
         * Reset everything when the reset button is clicked
         */
        private void btnReset_Click(object sender, EventArgs e)
        {
            pendingAddresses.Clear();
            addedAddresses.Clear();
            resultTable.Rows.Clear();
            txtResults.Clear();
            ipAddress.Clear();
            arpRequests.Clear();
            gratuitousArps.Clear();
            tblGratuitousArps.Rows.Clear();
            mapUrl = "https://maps.googleapis.com/maps/api/staticmap?size=500x450&markers=";
            markerCount = 0;
            numPackets = 0;
            txtPacketCount.Text = "0";
        }

        /**
         * Perform an IP lookup on the address that was entered
         */
        private void btnSearchIP_Click(object sender, EventArgs e)
        {
            addIP(ipAddress.Text, "", true);
        }
    }
}
