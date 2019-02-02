using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

    public enum SettingLevels
    {

        high = 0,
        medium,
        low

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

        public override string ToString()
        {
            return volumeName.ToString();
        }

        public StringBuilder volumeName;
        public Int32 serialNumber;
        public Int32 maxComponentLen;
        public Int32 fileSysetmFlags;
        public StringBuilder fileSystemName;

        public SettingLevels fileResolverMethod = SettingLevels.medium;
        public SettingLevels archiveMethod = SettingLevels.medium;

        [JsonIgnore]
        public string driveLetter;

    }

}
