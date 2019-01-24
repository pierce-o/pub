using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace pub
{

    class settingsClass
    {

        public string backupPath;

        public List<volumeInformation> whitelistedDrives;

        public SettingLevels fileResolverMethod;
        public SettingLevels archiveMethod;

    }

    class settingsManager
    {

        public settingsManager( ) // Constructor
        {
            settings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "pub\\settings.json"); // Set the local settings path
            settingsObject = new settingsClass(); // Create a new settins object
        }

        private string settings;
        private settingsClass settingsObject;

        public void readSettings() // Loads settings from the json file
        {
            using (var reader = new StreamReader(settings, false)) // Create a new reader under the settings file
            {
                string settingsData = reader.ReadToEnd(); // Read the whole file
                settingsObject = JsonConvert.DeserializeObject<settingsClass>(settingsData); // Convert from the json format to the settingsClass structure
            }
        }

        public void setBackupPath( string filePath ) 
        {
            settingsObject.backupPath = filePath; // Set the backup path to the one passed via the paramater 
            save(); // Save the new settings to the json file
        }

        private void save() // Converts and writes the settings object to the json file
        {
            using (var writer = new StreamWriter(settings, false)) // Create a new streamwriter for the settings file
                writer.Write( JsonConvert.SerializeObject( settingsObject , Formatting.Indented ) ); // Write the connverted and indented object to the file
        }

    }
}
