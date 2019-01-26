using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pub
{
    class FileBackup
    {

        public void backupHigh( SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            // When the USB is inserted it will backup all the files no matter what.
            switch( archiveMethod )
            {

                case SettingLevels.high:

                    string folderBackupPath = ""; // Get it from the JSON file.
                    string newFolderPath = Path.Combine( folderBackupPath, targetDrive.serialNumber.ToString("X8") );

                    if ( !Directory.Exists(newFolderPath) ) // If the path for the drives folder doesn't already exist create one so that we can backup there.
                        Directory.CreateDirectory( newFolderPath );

                    string datedFolderPath = Path.Combine( newFolderPath, DateTime.Now.ToShortDateString() );

                    if ( !Directory.Exists( datedFolderPath ) ) // Make sure we don't duplicate the data by backing it up more than once each day.
                        Directory.CreateDirectory( datedFolderPath );



                    break;

                case SettingLevels.medium:

                    break;

                case SettingLevels.low:

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }


        }

        public void backupMedium( SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            switch (archiveMethod)
            {

                case SettingLevels.high:



                    break;

                case SettingLevels.medium:

                    break;

                case SettingLevels.low:

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }

        }

        public void backupLow( SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            switch (archiveMethod)
            {

                case SettingLevels.high:



                    break;

                case SettingLevels.medium:

                    break;

                case SettingLevels.low:

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }

        }

    }
}
