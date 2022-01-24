using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using System.Text.RegularExpressions;

using Windows.Devices.Bluetooth.Advertisement;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the program
            var program = new Program();

        }

        static void WriteColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        public Program()
        {
            // Create Bluetooth Listener
            var watcher = new BluetoothLEAdvertisementWatcher();

            watcher.ScanningMode = BluetoothLEScanningMode.Active;

            // Only activate the watcher when we're recieving values >= -80
            watcher.SignalStrengthFilter.InRangeThresholdInDBm = -95;

            // Stop watching if the value drops below -90 (user walked away)
            watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -110;

            // Register callback for when we see an advertisements
            watcher.Received += OnAdvertisementReceived;

            // Wait 5 seconds to make sure the device is really out of range
            watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(5000);
            watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(2000);

            // Starting watching for advertisements
            watcher.Start();

            Console.WriteLine(String.Format("|{0,-20}|{1,-10}|{2,-30}|", "BLE ADDR", "RSSI", "Name"));
        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            // Tell the user we see an advertisement and print some properties
            ulong lMacAddr = eventArgs.BluetoothAddress;
            var rssi = eventArgs.RawSignalStrengthInDBm;
            string strMacAddr = String.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}",
                (lMacAddr >> (8 * 5)) & 0xff,
                (lMacAddr >> (8 * 4)) & 0xff,
                (lMacAddr >> (8 * 3)) & 0xff,
                (lMacAddr >> (8 * 2)) & 0xff,
                (lMacAddr >> (8 * 1)) & 0xff,
                (lMacAddr >> (8 * 0)) & 0xff);
            Console.Write("|");
            WriteColor(String.Format("{0,-20}", strMacAddr), ConsoleColor.DarkYellow);
            Console.Write("|");
            ConsoleColor rssi_color = ConsoleColor.White;
            if(rssi > -70)
            {
                rssi_color = ConsoleColor.Green;
            }
            else if(rssi > -85)
            {
                rssi_color = ConsoleColor.DarkYellow;
            }
            else
            {
                rssi_color = ConsoleColor.Red;
            }
            WriteColor(String.Format("{0,-10}", rssi), rssi_color);
            Console.Write("|");
            WriteColor(String.Format("{0,-30}", eventArgs.Advertisement.LocalName), ConsoleColor.DarkYellow);
            Console.Write("|\r\n");
        }
    }
}
