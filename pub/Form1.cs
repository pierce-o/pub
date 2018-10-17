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

namespace pub
{

    [StructLayout(LayoutKind.Sequential)]
    public class DEV_BROADCAST_VOLUME
    {
        public Int32 dbch_size;
        public Int32 dbch_devicetype;
        public Int32 dbch_reserved;
    }

    public partial class Form1 : Form
    {

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

                            
                            

                        }

                    }

                }

            }

            base.WndProc(ref m); // Call the original

        }

    }
}
