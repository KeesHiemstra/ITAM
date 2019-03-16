using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software_Explorer.Models
{
  public class SoftwareList
  {
    private bool InitialListFinished;

    public List<SoftwareItem> Items { get; set; } = new List<SoftwareItem>();
    public string ComputerName { get; private set; } = Environment.MachineName;

    public SoftwareList()
    {
      CollectLocalMachineAsync(false);
    }

    private async Task CollectLocalMachineAsync(bool WOW6432Node)
    {
      string uninstallKey;
      byte bits = 0;

      if (!WOW6432Node)
      {
        // x86 (32 bits) software list
        bits = 32;
        uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
      }
      else
      {
        // x64 (64 bits) software list
        bits = 64;
        uninstallKey = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
      }

      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(uninstallKey))
      {
        foreach (string subKeysNames in registryKey.GetSubKeyNames())
        {
          SoftwareItem si = new SoftwareItem();
          si.Source = "LocalMachine";
          si.Bits = bits;

          using (RegistryKey subRegistryKey = registryKey.OpenSubKey(subKeysNames))
          {
            si.SubKeyName = subRegistryKey.Name.Replace($"HKEY_LOCAL_MACHINE\\{uninstallKey}\\", "");

            si.DisplayName = await GetRegistryString(subRegistryKey, "DisplayName");
            si.DisplayVersion = await GetRegistryString(subRegistryKey, "DisplayVersion");
            si.Version = await GetRegistryString(subRegistryKey, "Version");
            si.EngineVersion = await GetRegistryString(subRegistryKey, "EngineVersion");
            si.BundleVersion = await GetRegistryString(subRegistryKey, "BundleVersion");
            si.Publisher = await GetRegistryString(subRegistryKey, "Publisher");
            si.DisplayIcon = await GetRegistryString(subRegistryKey, "DisplayIcon");
            si.InstallDate = await GetRegistryString(subRegistryKey, "InstallDate");
            si.InstallLocation = await GetRegistryString(subRegistryKey, "InstallLocation");
            si.UninstallString = await GetRegistryString(subRegistryKey, "UninstallString");
            si.ModifyPath = await GetRegistryString(subRegistryKey, "ModifyPath");
            si.RepairPath = await GetRegistryString(subRegistryKey, "RepairPath");
            si.URLInfoAbout = await GetRegistryString(subRegistryKey, "URLInfoAbout");
            si.HelpLink = await GetRegistryString(subRegistryKey, "HelpLink");
            si.WindowsInstaller = await GetRegistryBool(subRegistryKey, "WindowsInstaller");
            si.SystemComponent = await GetRegistryBool(subRegistryKey, "SystemComponent");
            si.NoRemove = await GetRegistryBool(subRegistryKey, "NoRemove");
            si.NoModify = await GetRegistryBool(subRegistryKey, "NoModify");
            si.NoRepair = await GetRegistryBool(subRegistryKey, "NoRepair");

          }//using registry subKey to collect the data

          //toDo: Skip adding when it is already existing
          Items.Add(si);
        }//for each subKeyName
      }//using registry key to query all installed software
      InitialListFinished = true;
    }

    private async Task<string> GetRegistryString(RegistryKey registryKey, string registryFieldName)
    {
      try
      {
        return registryKey.GetValue(registryFieldName).ToString();
      }
      catch
      {
        return null;
      }
    }

    private async Task<bool?> GetRegistryBool(RegistryKey registryKey, string registryFieldName)
    {
      try
      {
        return (bool)registryKey.GetValue(registryFieldName);
      }
      catch
      {
        return null;
      }
    }

  }
}
