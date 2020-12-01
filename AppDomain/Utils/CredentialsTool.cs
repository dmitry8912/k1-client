using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace K1_Insight.AppDomain.Utils
{
  public class CredentialsTool
  {
    public static void AddCredential(string host, string username, string password)
    {
      String szCmd = $"/c cmdkey.exe /generic:{host} /user:{username} /pass:{password}";
      ProcessStartInfo info = new ProcessStartInfo("cmd.exe", szCmd);
      var addCreds = new Process();
      info.WindowStyle = ProcessWindowStyle.Hidden;
      addCreds.StartInfo = info;
      addCreds.Start();
      addCreds.WaitForExit();
    }

    public static void DeleteCredentials(string host)
    {
      String szCmd = $"/c cmdkey.exe /delete:{host}";
      ProcessStartInfo info = new ProcessStartInfo("cmd.exe", szCmd);
      var addCreds = new Process();
      info.WindowStyle = ProcessWindowStyle.Hidden;
      addCreds.StartInfo = info;
      addCreds.Start();
      addCreds.WaitForExit();
    }
  }
}
