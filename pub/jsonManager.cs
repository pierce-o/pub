using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Forms;

namespace pub
{

    class settingsClass
    {

        public settingsClass()
        {
            whitelistedDrives = new List<volumeInformation>();
        }

        public string backupPath;

        public List<volumeInformation> whitelistedDrives;

    }

    class SettingsManager
    {

        public SettingsManager( ) // Constructor
        {
            settings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pub\\settings.json"); // Set the local settings path
            settingsObject = new settingsClass(); // Create a new settings object
        }

        private string settings;
        private settingsClass settingsObject;

        public settingsClass getSettingsObject()
        {
            return settingsObject;
        }

        public void readSettings() // Loads settings from the json file
        {
            using (var reader = new StreamReader(settings, false)) // Create a new reader under the settings file
            {
                string settingsData = reader.ReadToEnd(); // Read the whole file
                settingsObject = JsonConvert.DeserializeObject<settingsClass>(settingsData); // Convert from the json format to the settingsClass structure
            }
        }

        public volumeInformation findVolumeInformation( int serialnumber )
        {
            return settingsObject.whitelistedDrives.Find( x => x.serialNumber == serialnumber ); 
        }

        public int removeWhitelistedDevice( volumeInformation volumeinfo )
        {
            // This hopefully should only ever be 0 or 1
            int numberOfElementsRemoved = settingsObject.whitelistedDrives.RemoveAll( x => x.serialNumber == volumeinfo.serialNumber );

            save(); // Save this to the config file

            return numberOfElementsRemoved;
        }

        public bool addWhitelistDevice( volumeInformation volumeinfo )
        {
            readSettings(); // Make sure the settings object is upto date

            if (settingsObject.whitelistedDrives.Find(x => x.serialNumber == volumeinfo.serialNumber) == null) // Check if the device is already in the list
            {
                settingsObject.whitelistedDrives.Add(volumeinfo); // Add it to the end of the list
                save(); // Save it to the json file
                return true; 
            }
            else
            {
                MessageBox.Show("Device is already whitelisted.");
                return false;
            }

        }

        public void setBackupPath( string filePath ) 
        {
            settingsObject.backupPath = filePath; // Set the backup path to the one passed via the paramater 
            save(); // Save the new settings to the json file
        }

        public void setArchiveMethod(volumeInformation device, SettingLevels level)
        {
            settingsObject.whitelistedDrives.Find(x => x.serialNumber == device.serialNumber).archiveMethod = level;
            save(); // Save the new settings to the json file
        }

        public void setFileResolver(volumeInformation device, SettingLevels level)
        { 
            settingsObject.whitelistedDrives.Find(x => x.serialNumber == device.serialNumber).fileResolverMethod = level;
            save(); // Save the new settings to the json file
        }

        public SettingLevels getArchiveMethod(volumeInformation device)
        { 
            return settingsObject.whitelistedDrives.Find(x => x.serialNumber == device.serialNumber).archiveMethod;
        }

        public SettingLevels getFileResolver(volumeInformation device)
        {
            return settingsObject.whitelistedDrives.Find(x => x.serialNumber == device.serialNumber).fileResolverMethod;
        }

        public void setWhitelistedDevice(volumeInformation device)
        {
            settingsObject.whitelistedDrives.RemoveAll(x => x.serialNumber == device.serialNumber);
            settingsObject.whitelistedDrives.Add( device );
            save();
        }

        private void save() // Converts and writes the settings object to the json file
        {
            using (var writer = new StreamWriter(settings, false)) // Create a new streamwriter for the settings file
                writer.Write( JsonConvert.SerializeObject( settingsObject , Formatting.Indented ) ); // Write the connverted and indented object to the file
        }

    }
}
