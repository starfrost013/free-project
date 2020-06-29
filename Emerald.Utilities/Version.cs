using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Emerald.Utilities
{
    /// <summary>
    /// Copyright © 2020 avant-gardé eyes
    /// 
    /// Emerald version class
    /// </summary>
    public class EVersion
    {
        public DateTime BuildDate { get; set; }
        public string BuildOwner { get; set; } // eg "Cosmo"
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; } // The build number
        public int Revision { get; set; }
        public string Status { get; set; } // eg "Beta"


        public void GetGameVersion()
        {
            GetVersion(); 
            GetBuildDate();
        }

        /// <summary>
        /// Gets the version by obtaining the version of the free! executable.
        /// </summary>
        private void GetVersion()
        {
            // Entry assembly is 
            Assembly NetAsm = Assembly.GetEntryAssembly();
            FileVersionInfo FVI = FileVersionInfo.GetVersionInfo(NetAsm.Location);

            List<int> _ = Utils.SplitVersion(FVI.FileVersion);

            Major = _[0];
            Minor = _[1];
            Build = _[2];
            Revision = _[3];
        }

        private void GetBuildDate()
        {
            BuildDate = DateTime.Parse(Properties.Resources.CoreBuildDate);
        }

        // Not multiplatform but works for now
        private void GetBuildOwner()
        {
            BuildOwner = WindowsIdentity.GetCurrent().Name;
        }
    }
}
