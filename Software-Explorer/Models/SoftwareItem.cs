using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Explorer.Models
{
  public class SoftwareItem
  {
    /// <summary>
    /// 32: Only available in 32 bit.
    /// 64: Only available in 64 bit.
    /// 96: Exists in both in 32 and 64 bit.
    /// </summary>
    public byte Bits { get; set; }

    /// <summary>
    /// 'Local Machine': Available for all users.
    /// User name: Only available for this user name.
    /// </summary>
    public string Source { get; set; }
    public string SubKeyName { get; set; }

    public string DisplayName { get; set; }
    public string DisplayVersion { get; set; }
    public string Version { get; set; }
    public string EngineVersion { get; set; }
    public string BundleVersion { get; set; }
    public string Publisher { get; set; }
    public string DisplayIcon { get; set; }
    public string InstallDate { get; set; }
    public string InstallLocation { get; set; }
    public string UninstallString { get; set; }
    public string ModifyPath { get; set; }
    public string RepairPath { get; set; }
    public string URLInfoAbout { get; set; }
    public string HelpLink { get; set; }
    public bool? WindowsInstaller { get; set; }
    public bool? SystemComponent { get; set; }
    public bool? NoRemove { get; set; }
    public bool? NoModify { get; set; }
    public bool? NoRepair { get; set; }

  }
}
