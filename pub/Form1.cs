using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32; // Used for the registery
using System.IO; // Used for getting all the current drives

namespace pub
{

    public partial class Form1 : Form
    {

        /* WinAPI imports*/

        [DllImport("kernel32.dll")]
        static extern bool GetVolumeInformation( string lpRootPathName, StringBuilder lpVolumeNameBuffer, Int32 nVolumeNameSize, out Int32 lpVolumeSerialNumber, out Int32 lpMaximumComponentLength, out Int32 lpFileSystemFlags, StringBuilder lpFileSystemNameBuffer, Int32 nFileSystemNameSize );

        /* Required WNDPROC  */

        const int WM_DEVICECHANGE = 0x219;
        const int DBT_DEVICEARRIVAL = 0x8000;
        
        // 
        const int DBT_DEVTYP_VOLUME = 0x2;

        // Flags used for identifying what kind of volume the inserted device is.
        const int DBTF_MEDIA = 0x1; // This is a CD or a USB flash drive.
        const int DBTF_NET = 0x2; // This is a networked drive.

        List<volumeInformation> connectedDrives = new List<volumeInformation>();

        public Form1()
        {
            InitializeComponent();

            // Populate the connected devices.

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    listBoxDevicesConnected.Items.Add(drive.Name + drive.VolumeLabel);

                    volumeInformation volumeInfo = new volumeInformation();

                    if (GetVolumeInformation(drive.RootDirectory.ToString(), volumeInfo.volumeName, volumeInfo.volumeName.Capacity, out volumeInfo.serialNumber, out volumeInfo.maxComponentLen, out volumeInfo.fileSysetmFlags, volumeInfo.fileSystemName, volumeInfo.fileSystemName.Capacity))
                        connectedDrives.Add(volumeInfo);

                }
            }

            // Fetch and popluate the whitelisted devices.

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", false);

            if (registryKey != null) // Create that sub key
            {
                // Proceed to check if the device has been added to the pool

                string[] devices = registryKey.GetSubKeyNames();

                foreach (string device in devices)
                {

                    RegistryKey rk = registryKey.OpenSubKey(device, false);

                    listBoxWhitelistedDevices.Items.Add( rk.GetValue("name").ToString() );

                    rk.Close();

                }

                registryKey.Close();
            }

        }

        public string convertToDriveLetter(int mask)
        {

            int i; // Uninitialized at the moment as it gets set at the start of the for loop.

            for ( i = 0; i < 26; i++ ) // Loop for all the possible drive letters which is 26 
            {

                if ( (mask & 0x1) == 1 ) // Check if the mask is currently 1 which means that the letter is at this offset.
                    break; // Exit the loop.
                else
                    mask = mask >> 1; // Shift the mask over one to check on the next loop.

            }

            // A is the base offset of the char since the first capital letter of the ascii table then each letter after that is one up each time.
            return ( (char)( Convert.ToChar( 'A' + i ) ) ).ToString(); 

        }

        protected override void WndProc(ref Message m)
        {

            int wparam = m.WParam != null ? m.WParam.ToInt32() : 0; 
 
            if ( m.Msg == WM_DEVICECHANGE && wparam != 0 ) // Check that the received message is for device event
            {

                if( wparam == DBT_DEVICEARRIVAL) // If the messages is due to a device being inserted
                { 
                        
                    if( m.LParam != null ) // Make sure the LParam isn't null
                    {

                        DEV_BROADCAST_VOLUME hdr = new DEV_BROADCAST_VOLUME(); // New class to store the data of LParam 
                        Marshal.PtrToStructure( m.LParam, hdr ); // Cast the LParam data to the needed structutre

                        if (hdr.dbch_devicetype == DBT_DEVTYP_VOLUME) // The type is a USB 
                        {

                            // Now that we have idenified that it is a volume type we need to get the specifc
                            // information about the drive.

                            string driveLetter = convertToDriveLetter(hdr.dbcv_unitmask) + ":"; // ":" is needed for the GetVolumeInformationA function.

                            volumeInformation volumeInfo = new volumeInformation();

                            if (GetVolumeInformation(driveLetter, volumeInfo.volumeName, volumeInfo.volumeName.Capacity, out volumeInfo.serialNumber, out volumeInfo.maxComponentLen, out volumeInfo.fileSysetmFlags, volumeInfo.fileSystemName, volumeInfo.fileSystemName.Capacity))
                            {

                                listBoxDevicesConnected.Items.Add( driveLetter + "\\" + volumeInfo.volumeName ); // Add the newest device to the end of the device list.

                                connectedDrives.Add(volumeInfo);

                                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", false);

                                if (registryKey == null) // Create that sub key
                                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\pub\devices");
                                else
                                {
                                    // Proceed to check if the device has been added to the pool

                                    string[] devices = registryKey.GetSubKeyNames();

                                    foreach (string device in devices)
                                    {

                                        RegistryKey rk = registryKey.OpenSubKey(device, false);

                                        if (rk != null)
                                        {
                                            if ((string)rk.GetValue("serialnumber") == volumeInfo.serialNumber.ToString("X8"))
                                            {
                                                Debug.WriteLine("Serial Number found.");
                                                break;
                                            }
                                        }

                                        rk.Close();

                                    }

                                    registryKey.Close();
                                }

                            }

                        }

                    }

                }

            }

            base.WndProc(ref m); // Call the original

        }

        private void buttonBackupLocation_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fileDialog = new FolderBrowserDialog();

            DialogResult result = fileDialog.ShowDialog();

            if ( result == DialogResult.OK )
            {
                // Set the path in the settings to the new path
                // Todo - have an ini file within the appdata folder

                textBoxFolderPath.Text = fileDialog.SelectedPath;

                // Force the textbox to show the end of the file path
                textBoxFolderPath.SelectionStart = textBoxFolderPath.Text.Length;
                textBoxFolderPath.SelectionLength = 0;
            }

        }

        private void buttonWhitelist_Click(object sender, EventArgs e)
        {

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", true);

            if (registryKey != null)
            {
                // Proceed to check if the device has been added to the pool

                foreach (volumeInformation drive in connectedDrives) // Loop through the stored devices
                {

                    // Todo - find a better method of identifying the drive aka serial number. Maybe a class for the combobox item with a ToString override?

                    // Check if the current device in the array has the same volume name as the selected drive.
                    if ( drive.volumeName.ToString() == listBoxDevicesConnected.SelectedItem.ToString().Substring(3) ) // Remove the devices path
                    {

                        if ( !(registryKey.GetSubKeyNames().Contains(drive.serialNumber.ToString("X8"))) ) // Make sure that the device isn't already registered.
                        {
                            using (RegistryKey rk = registryKey.CreateSubKey(drive.serialNumber.ToString("X8"), true)) // Create a new sub key with the serial number as the name.
                            {

                                if (rk != null) // Make sure that it has been created.
                                {
                                    // Set the necessary values.
                                    rk.SetValue("name", drive.volumeName);
                                    rk.SetValue("serialnumber", drive.serialNumber.ToString("X8"));
                                    // Add the device name to the list of whitelisted devices.
                                    listBoxWhitelistedDevices.Items.Add(listBoxDevicesConnected.SelectedItem.ToString().Substring(3));
                                }

                                rk.Close(); // Close the opened handle to the registry.

                            }
                        }
                        else // Notify the user that it already exists
                            MessageBox.Show("Device already is whitelisted!");

                    }

                }

                registryKey.Close(); // Close the opened handle to the registry.

            }   

        }

        private void buttonRemoveDevice_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to remove this device?", "Device Removal", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Remove it from the registry 

                // Get the volume info

                // Remove it visually
                listBoxWhitelistedDevices.Items.RemoveAt(listBoxWhitelistedDevices.SelectedIndex);

            }

        }
    }
}
