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

        FileBackup backupProcesses;
        SettingsManager settingsManager;

        public string targetSettingsFolder = null;
        public string targetSettingsPath = null;

        public Form1()
        {
            InitializeComponent();

            backupProcesses = new FileBackup();
            settingsManager = new SettingsManager();

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
            {
                settingsManager.readSettings();

                // Just apply the settings to the visual elements

                // Path textbox 
                textBoxFolderPath.Text = settingsManager.getSettingsObject().backupPath;

                // Force the textbox to show the end of the file path
                textBoxFolderPath.SelectionStart = textBoxFolderPath.Text.Length;
                textBoxFolderPath.SelectionLength = 0;
            }

            // Adding already connected drives to the program

            foreach (DriveInfo drive in DriveInfo.GetDrives()) // Loop through each drive connected to the PC
            {
                if (drive.DriveType == DriveType.Removable) // Check if it is a USB
                {
                    volumeInformation volumeInfo = new volumeInformation();

                    volumeInfo.driveLetter = drive.RootDirectory.ToString();

                    if (GetVolumeInformation(drive.RootDirectory.ToString(), volumeInfo.volumeName, volumeInfo.volumeName.Capacity, out volumeInfo.serialNumber, out volumeInfo.maxComponentLen, out volumeInfo.fileSysetmFlags, volumeInfo.fileSystemName, volumeInfo.fileSystemName.Capacity))
                        listBoxDevicesConnected.Items.Add( volumeInfo ); // Add the volume information for the new drive to the end of the listbox
                }
            }

            // Fetch and popluate the whitelisted devices.

            if( settingsManager.getSettingsObject().whitelistedDrives != null ) // Make sure there is a list in the first place
                foreach ( var device in settingsManager.getSettingsObject().whitelistedDrives ) // Loop through all the subkeys 
                    listBoxWhitelistedDevices.Items.Add( device ); // Add each one to the end of the whitelisted devices 

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

                                volumeInfo.driveLetter = driveLetter + "\\";

                                if ( listBoxDevicesConnected.Items.Contains( volumeInfo ) ) // Make sure that the device isn't already in the listbox
                                    listBoxDevicesConnected.Items.Add( volumeInfo ); // Add the newest device to the end of the device list.

                                volumeInformation currentWhitelistedObject = settingsManager.findVolumeInformation( volumeInfo.serialNumber );

                                IEnumerable< DriveInfo > driveInfo = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Removable);

                                if (driveInfo != null)
                                {
                                    volumeInfo.unusedSpace = driveInfo.Where( x => x.VolumeLabel == volumeInfo.volumeName.ToString()).First().AvailableFreeSpace;
                                    settingsManager.setWhitelistedDevice( volumeInfo );
                                }

                                if (currentWhitelistedObject != null ) // Drive was found inside the whitelisted list
                                {

                                    switch (currentWhitelistedObject.fileResolverMethod)
                                    {

                                        case SettingLevels.high:
                                            backupProcesses.backupHigh(settingsManager.getSettingsObject().backupPath, currentWhitelistedObject.archiveMethod, volumeInfo);
                                            break;

                                        case SettingLevels.medium:
                                            backupProcesses.backupMedium(settingsManager.getSettingsObject().backupPath, currentWhitelistedObject.archiveMethod, volumeInfo);
                                            break;

                                        case SettingLevels.low:
                                            backupProcesses.backupLow(settingsManager.getSettingsObject().backupPath, currentWhitelistedObject.archiveMethod, volumeInfo);
                                            break;

                                        default:
                                            MessageBox.Show("Error occured when getting the resolver setting.");
                                            break;

                                    }

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

                settingsManager.setBackupPath( fileDialog.SelectedPath );

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
                // Save the device to the settings.json and add it to the list
                settingsManager.addWhitelistDevice( (volumeInformation)listBoxDevicesConnected.SelectedItem );
                // Add it visually to the whitelisted device listbox
                listBoxWhitelistedDevices.Items.Add( listBoxDevicesConnected.SelectedItem );
            }
            else // Tell the user that they haven't selected a device
                MessageBox.Show("No device has been selected. Select a device then try again.");

        }

        private void buttonRemoveDevice_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to remove this device?", "Device Removal", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Remove it from the settings.json file and the device list 
                // Check if the fuction returns 1 as it means that one device has been removed 
                if(settingsManager.removeWhitelistedDevice( (volumeInformation)listBoxWhitelistedDevices.SelectedItem ) == 1)
                    listBoxWhitelistedDevices.Items.RemoveAt( listBoxWhitelistedDevices.SelectedIndex ); // Remove it visually       
            }

        }

        private void comboBoxFileResolver_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure the selected item isn't null
            if(listBoxWhitelistedDevices.SelectedItem != null) // Update the FileResolver value for the current selected device 
                settingsManager.setFileResolver((volumeInformation)listBoxWhitelistedDevices.SelectedItem, (SettingLevels)comboBoxFileResolver.SelectedIndex);
        }

        private void comboBoxArchiveMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Make sure the selected item isn't null
            if (listBoxWhitelistedDevices.SelectedItem != null) // Update the ArchiveMethod value for the current selected device 
                settingsManager.setArchiveMethod((volumeInformation)listBoxWhitelistedDevices.SelectedItem, (SettingLevels)comboBoxArchiveMethod.SelectedIndex);
        }

        private void listBoxWhitelistedDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the combobox's value for the corresponding selected device 
            comboBoxArchiveMethod.SelectedIndex = (int)settingsManager.getArchiveMethod( (volumeInformation)listBoxWhitelistedDevices.SelectedItem );
            comboBoxFileResolver.SelectedIndex = (int)settingsManager.getFileResolver((volumeInformation)listBoxWhitelistedDevices.SelectedItem);
        }
    }
}
