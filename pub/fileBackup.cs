using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace pub
{
    class FileBackup
    {

        public void backupHigh(string backupPath, SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            string newFolderPath = Path.Combine(backupPath, targetDrive.serialNumber.ToString("X8"));

            if (!Directory.Exists(newFolderPath)) // If the path for the drives folder doesn't already exist create one so that we can backup there.
                Directory.CreateDirectory(newFolderPath);

            string currentDate = DateTime.Now.ToShortDateString();
            currentDate = currentDate.Replace("/", "-");

            string datedFolderPath = Path.Combine(newFolderPath, currentDate);

            // Get information about the USB drives files and folders
            DirectoryInfo directoryInfo = new DirectoryInfo(targetDrive.driveLetter);

            // When the USB is inserted it will backup all the files no matter what.
            switch ( archiveMethod )
            {

                case SettingLevels.high:

                    if (!Directory.Exists(datedFolderPath)) // Make sure we don't duplicate the data by backing it up more than once each day.
                    {
                        Directory.CreateDirectory(datedFolderPath);

                        // Replicate all the folders from the USB 
                        foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                        {
                            if (dir.Name != "System Volume Information") // Skip this hidden system folder
                            {
                                // Remove the USB drive letter from the folders full path
                                string newDir = Path.Combine(datedFolderPath, dir.FullName.Substring(3));

                                // Make sure it isn't already there
                                if (!Directory.Exists(newDir))
                                {
                                    Directory.CreateDirectory(newDir); // Create the folder
                                }
                            }
                        }

                        // Get all files from the USB drive
                        foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                        {
                            if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                            {
                                string newPath = Path.Combine(datedFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                                if (!File.Exists(newPath)) // Make sure it hasn't already been backed up
                                    File.Copy(file.FullName, newPath); // Copy it over to the new location
                            }
                        }
                    }

                    break;

                case SettingLevels.medium:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {

                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            if (!File.Exists(newPath) || file.LastWriteTime.CompareTo(DateTime.Now.AddHours(-1)) > 0) // Make sure it hasn't already been backed up
                                File.Copy(file.FullName, newPath, true); // Copy it over to the new location
                        }
                    }

                    break;

                case SettingLevels.low:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {
                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            File.Copy(file.FullName, newPath, true); // Copy it over to the new location and overwrite it
                        }
                    }

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }


        }

        public void backupMedium( string backupPath, SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            string newFolderPath = Path.Combine(backupPath, targetDrive.serialNumber.ToString("X8"));

            if (!Directory.Exists(newFolderPath)) // If the path for the drives folder doesn't already exist create one so that we can backup there.
                Directory.CreateDirectory(newFolderPath);

            string currentDate = DateTime.Now.ToShortDateString();
            currentDate = currentDate.Replace("/", "-");

            string datedFolderPath = Path.Combine(newFolderPath, currentDate);

            // Get information about the USB drives files and folders
            DirectoryInfo directoryInfo = new DirectoryInfo(targetDrive.driveLetter);

            // When the USB is inserted it will backup all the files no matter what.
            switch (archiveMethod)
            {

                case SettingLevels.high:

                    if (!Directory.Exists(datedFolderPath)) // Make sure we don't duplicate the data by backing it up more than once each day.
                    {
                        Directory.CreateDirectory(datedFolderPath);

                        // Replicate all the folders from the USB 
                        foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                        {
                            if (dir.Name != "System Volume Information") // Skip this hidden system folder
                            {
                                // Remove the USB drive letter from the folders full path
                                string newDir = Path.Combine(datedFolderPath, dir.FullName.Substring(3));

                                // Make sure it isn't already there
                                if (!Directory.Exists(newDir))
                                {
                                    Directory.CreateDirectory(newDir); // Create the folder
                                }
                            }
                        }

                        // Get all files from the USB drive
                        foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                        {

                            if (file.LastWriteTime.CompareTo(DateTime.Now.AddDays(-1)) > 0)
                                break;

                            if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                            {
                                string newPath = Path.Combine(datedFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                                if (!File.Exists(newPath)) // Make sure it hasn't already been backed up
                                    File.Copy(file.FullName, newPath); // Copy it over to the new location
                            }
                        }
                    }

                    break;

                case SettingLevels.medium:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {

                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            if (!File.Exists(newPath) || file.LastWriteTime.CompareTo(DateTime.Now.AddHours(-1)) > 0) // Make sure it hasn't already been backed up
                                File.Copy(file.FullName, newPath, true); // Copy it over to the new location
                        }
                    }

                    break;

                case SettingLevels.low:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {

                        if (file.LastWriteTime.CompareTo(DateTime.Now.AddDays(-1)) > 0)
                            break;

                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {
                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            File.Copy(file.FullName, newPath, true); // Copy it over to the new location and overwrite it
                        }
                    }

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }

        }

        public void backupLow(string backupPath, SettingLevels archiveMethod, volumeInformation targetDrive )
        {

            string newFolderPath = Path.Combine(backupPath, targetDrive.serialNumber.ToString("X8"));

            if (!Directory.Exists(newFolderPath)) // If the path for the drives folder doesn't already exist create one so that we can backup there.
                Directory.CreateDirectory(newFolderPath);

            string currentDate = DateTime.Now.ToShortDateString();
            currentDate = currentDate.Replace("/", "-");

            string datedFolderPath = Path.Combine(newFolderPath, currentDate);

            // Get information about the USB drives files and folders
            DirectoryInfo directoryInfo = new DirectoryInfo(targetDrive.driveLetter);

            IEnumerable<DriveInfo> driveInfo = DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Removable);

            if (driveInfo == null)
                return;

            if (targetDrive.unusedSpace != driveInfo.Where(x => x.VolumeLabel == targetDrive.volumeName.ToString()).First().AvailableFreeSpace)
                return;

            // When the USB is inserted it will backup all the files no matter what.
            switch (archiveMethod)
            {

                case SettingLevels.high:

                    if (!Directory.Exists(datedFolderPath)) // Make sure we don't duplicate the data by backing it up more than once each day.
                    {
                        Directory.CreateDirectory(datedFolderPath);

                        // Replicate all the folders from the USB 
                        foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                        {
                            if (dir.Name != "System Volume Information") // Skip this hidden system folder
                            {
                                // Remove the USB drive letter from the folders full path
                                string newDir = Path.Combine(datedFolderPath, dir.FullName.Substring(3));

                                // Make sure it isn't already there
                                if (!Directory.Exists(newDir))
                                {
                                    Directory.CreateDirectory(newDir); // Create the folder
                                }
                            }
                        }

                        // Get all files from the USB drive
                        foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                        {
                            if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                            {
                                string newPath = Path.Combine(datedFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                                if (!File.Exists(newPath)) // Make sure it hasn't already been backed up
                                    File.Copy(file.FullName, newPath); // Copy it over to the new location
                            }
                        }
                    }

                    break;

                case SettingLevels.medium:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {

                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            if (!File.Exists(newPath) || file.LastWriteTime.CompareTo(DateTime.Now.AddHours(-1)) > 0) // Make sure it hasn't already been backed up
                                File.Copy(file.FullName, newPath, true); // Copy it over to the new location
                        }
                    }

                    break;

                case SettingLevels.low:

                    // Replicate all the folders from the USB 
                    foreach (DirectoryInfo dir in directoryInfo.GetDirectories("*.*", SearchOption.AllDirectories))
                    {
                        if (dir.Name != "System Volume Information") // Skip this hidden system folder
                        {
                            // Remove the USB drive letter from the folders full path
                            string newDir = Path.Combine(newFolderPath, dir.FullName.Substring(3));

                            // Make sure it isn't already there
                            if (!Directory.Exists(newDir))
                            {
                                Directory.CreateDirectory(newDir); // Create the folder
                            }
                        }
                    }

                    // Get all files from the USB drive
                    foreach (FileInfo file in directoryInfo.GetFiles("*.*", SearchOption.AllDirectories))
                    {

                        if (file.LastWriteTime.CompareTo(DateTime.Now.AddDays(-1)) > 0)
                            break;

                        if (file.Name != "WPSettings.dat" && file.Name != "IndexerVolumeGuid") // Skip the hidden system files
                        {
                            string newPath = Path.Combine(newFolderPath, file.FullName.Substring(3)); // Combine the backup location with the file path without the drive letter 
                            File.Copy(file.FullName, newPath, true); // Copy it over to the new location and overwrite it
                        }
                    }

                    break;

                default:
                    MessageBox.Show("Unknown archiveMethod");
                    break;

            }

        }

    }
}
