using K1_Insight.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace K1_Insight.AppDomain.Utils
{
  public class RegistryTool
  {
    public static void SetUrlHandler()
    {
      if(Registry.ClassesRoot.OpenSubKey("K1") == null)
      {
        using (StreamWriter s = new StreamWriter("k1.reg"))
        {
          s.Write(Resources.k1_url_handler.Replace("{PATH}", Environment.CurrentDirectory.Replace("\\", "\\\\") + "\\\\" + "K1-Insight.exe"));
          s.Close();
          var process = Process.Start("regedit.exe", "/s " + Environment.CurrentDirectory + "\\k1.reg");
          process.WaitForExit();
          s.Dispose();
          File.Delete("k1.reg");
        }
      }
    }
  }
}
