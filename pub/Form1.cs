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

namespace pub
{

    [StructLayout(LayoutKind.Sequential)]
    public class DEV_BROADCAST_VOLUME
    {
        public Int32 dbch_size;
        public Int32 dbch_devicetype;
        public Int32 dbch_reserved;
        public Int32 dbcv_unitmask;
    }

    public class volumeInformation
    {

        public volumeInformation()
        {
            volumeName = new StringBuilder(261);
            serialNumber = -1337;
            maxComponentLen = -1337;
            fileSysetmFlags = -1337;
            fileSystemName = new StringBuilder(261);
        }
        

        public StringBuilder volumeName;
        public Int32 serialNumber;
        public Int32 maxComponentLen;
        public Int32 fileSysetmFlags;
        public StringBuilder fileSystemName;

    }

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

        public Form1()
        {
            InitializeComponent();
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

                            if(GetVolumeInformation(driveLetter, volumeInfo.volumeName, volumeInfo.volumeName.Capacity, out volumeInfo.serialNumber, out volumeInfo.maxComponentLen, out volumeInfo.fileSysetmFlags, volumeInfo.fileSystemName, volumeInfo.fileSystemName.Capacity ))
                            {

                                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\pub\devices");

                                if(registryKey == null) // Create that sub key
                                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\pub\devices");
                                else
                                {
                                    // Proceed to check if the device has been added to the pool

                                    string[] devices = registryKey.GetSubKeyNames();

                                    foreach( string device in devices )
                                    {

                                        RegistryKey rk = registryKey.OpenSubKey( device, false );

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
            
                            Debug.WriteLine(volumeInfo.serialNumber.ToString("X8"));

                           

                        }

                    }

                }

            }

            base.WndProc(ref m); // Call the original

        }

    }
}
