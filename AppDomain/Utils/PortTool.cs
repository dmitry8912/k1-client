using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
namespace K1_Insight.AppDomain.Utils
{
  public class PortTool
  {
    public static int GetFreePort(int startingPort = 12000)
    {
      IPEndPoint[] endPoints;
      List<int> portArray = new List<int>();

      IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

      TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
      portArray.AddRange(from n in connections
                         where n.LocalEndPoint.Port >= startingPort
                         select n.LocalEndPoint.Port);

      endPoints = properties.GetActiveTcpListeners();
      portArray.AddRange(from n in endPoints
                         where n.Port >= startingPort
                         select n.Port);

      endPoints = properties.GetActiveUdpListeners();
      portArray.AddRange(from n in endPoints
                         where n.Port >= startingPort
                         select n.Port);

      portArray.Sort();

      for (int i = startingPort; i < UInt16.MaxValue; i++)
        if (!portArray.Contains(i))
          return i;

      return 0;
    }
  }
}
