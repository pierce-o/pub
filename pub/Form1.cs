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
using Newtonsoft.Json;

/*
 * 
 *  Aim to change the the regisry to use a json file
 *  One json file for everything
 *  
 *  Optimize it by removing all the useless loops
 * 
 */

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

        fileBackup backupProcesses;
        settingsManager settingsManager;

        public string targetSettingsFolder = null;
        public string targetSettingsPath = null;

        public Form1()
        {
            InitializeComponent();

            backupProcesses = new fileBackup();
            settingsManager = new settingsManager();

            // Populate the connected devices.

            // Assign the path for the settings path
            targetSettingsPath = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "pub\\settings.json");
            targetSettingsFolder = Path.Combine(Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "pub");

            if (File.Exists(targetSettingsPath) == false || Directory.Exists(targetSettingsFolder) == false) // If the settings file doesn't currently exist then create it
            {

                Directory.CreateDirectory(targetSettingsFolder); // Create the folder in appdata
                File.Create(targetSettingsPath).Close(); // Create the json file

                // Set the default file backup path to appdata pub
                settingsManager.setBackupPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pub\\backups"));

            }
            else // If there is already a file for it then just read the file and populate the settings object
                settingsManager.readSettings();

            // Adding already connected drives to the program

            foreach (DriveInfo drive in DriveInfo.GetDrives()) // Loop through each drive connected to the PC
            {
                if (drive.DriveType == DriveType.Removable) // Check if it is a USB
                {
                    listBoxDevicesConnected.Items.Add(drive.Name + drive.VolumeLabel); // Add it to the connected drives listbox

                    volumeInformation volumeInfo = new volumeInformation();

                    if (GetVolumeInformation(drive.RootDirectory.ToString(), volumeInfo.volumeName, volumeInfo.volumeName.Capacity, out volumeInfo.serialNumber, out volumeInfo.maxComponentLen, out volumeInfo.fileSysetmFlags, volumeInfo.fileSystemName, volumeInfo.fileSystemName.Capacity))
                        connectedDrives.Add(volumeInfo); // Add the volume information for the new drive to the end of the connectedDrives list

                }
            }

            // Fetch and popluate the whitelisted devices.

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", false);

            if (registryKey != null) // Create that sub key
            {
                // Proceed to check if the device has been added to the pool

                string[] devices = registryKey.GetSubKeyNames();

                foreach (string device in devices) // Loop through all the subkeys 
                {

                    RegistryKey rk = registryKey.OpenSubKey(device, false); 

                    listBoxWhitelistedDevices.Items.Add( rk.GetValue("name").ToString() ); // Add each one to the end of the whitelisted devices 

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

                                                SettingLevels archiveMethod = (SettingLevels)comboBoxArchiveMethod.SelectedIndex;
                                                SettingLevels fileResolver = (SettingLevels)comboBoxFileResolver.SelectedIndex;


                                                switch (fileResolver)
                                                {

                                                    case SettingLevels.high:
                                                        backupProcesses.backupHigh(archiveMethod, volumeInfo);
                                                        break;

                                                    case SettingLevels.medium:
                                                        backupProcesses.backupMedium(archiveMethod, volumeInfo);
                                                        break;

                                                    case SettingLevels.low:
                                                        backupProcesses.backupLow(archiveMethod, volumeInfo);
                                                        break;

                                                    default:
                                                        MessageBox.Show("Error occured when getting the resolver setting.");
                                                        break;

                                                }

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

            FolderBrowserDialog fileDialog = new FolderBrowserDialog(); // Create a new file dialog

            DialogResult result = fileDialog.ShowDialog(); // Open it and get the result from it

            if ( result == DialogResult.OK ) // If it has been selected and confirmed
            {
                // Set the path in the settings to the new path

                textBoxFolderPath.Text = fileDialog.SelectedPath;

                // Force the textbox to show the end of the file path
                textBoxFolderPath.SelectionStart = textBoxFolderPath.Text.Length;
                textBoxFolderPath.SelectionLength = 0;
            }

        }

        private void buttonWhitelist_Click(object sender, EventArgs e)
        {

            if (listBoxDevicesConnected.SelectedItem != null)
            {

                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", true);

                if (registryKey != null)
                {
                    // Proceed to check if the device has been added to the pool

                    foreach (volumeInformation drive in connectedDrives) // Loop through the stored devices
                    {

                        // Todo - find a better method of identifying the drive aka serial number. Maybe a class for the combobox item with a ToString override?

                        // Check if the current device in the array has the same volume name as the selected drive.
                        if (drive.volumeName.ToString() == listBoxDevicesConnected.SelectedItem.ToString().Substring(3)) // Remove the devices path
                        {

                            if (!(registryKey.GetSubKeyNames().Contains(drive.serialNumber.ToString("X8")))) // Make sure that the device isn't already registered.
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
            else
                MessageBox.Show("No device has been selected. Select a device then try again.");

        }

        private void buttonRemoveDevice_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to remove this device?", "Device Removal", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                // Remove it from the registry 

                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices", true);

                if (registryKey != null)
                {

                    foreach (volumeInformation drive in connectedDrives) // Loop through the stored devices
                    {

                        // Todo - find a better method of identifying the drive aka serial number. Maybe a class for the combobox item with a ToString override?

                        // Check if the current device in the array has the same volume name as the selected drive.
                        if (drive.volumeName.ToString() == listBoxWhitelistedDevices.SelectedItem.ToString()) // Remove the devices path
                        {

                            if (registryKey.GetSubKeyNames().Contains(drive.serialNumber.ToString("X8"))) // Make sure that the device is already registered.
                                registryKey.DeleteSubKey(drive.serialNumber.ToString("X8")); // Delete the devices subkey
                            else // Notify the user that it already doesn't exist
                                MessageBox.Show("Device not found in registry!");

                        }

                    }

                    registryKey.Close(); // Close the opened handle to the registry.

                }
            
                // Remove it visually
                listBoxWhitelistedDevices.Items.RemoveAt(listBoxWhitelistedDevices.SelectedIndex);

            }

        }
    }
}
