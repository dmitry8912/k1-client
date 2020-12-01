using K1_Insight.AppDomain.Models;
using System;
using Renci.SshNet;
using System.Diagnostics;
using K1_Insight.AppDomain.Utils;
using System.Threading;

namespace K1_Insight.AppDomain.Implementations
{
  public class SshWrapper
  {
    private Endpoint endpoint;
    public SshClient client;
    public ForwardedPortLocal port;
    public Process rdcProcess;
    public Thread rdcThread;
    public Thread extenderThread;
    public SshWrapper(Endpoint endpoint)
    {
      this.endpoint = endpoint;
    }

    public void Connect()
    {
      var gatewayParams = endpoint.gateway.url.Split(':');
      var gateway = gatewayParams[0];
      var gatewayPort = Convert.ToInt32(gatewayParams[1]);
      var connectionInfo = new ConnectionInfo(gateway, gatewayPort, endpoint.gateway.credential.name, 
        new PasswordAuthenticationMethod(endpoint.gateway.credential.name, endpoint.gateway.credential.plainPassword));
      this.client = new SshClient(connectionInfo);
      client.Connect();

      var portNumber = PortTool.GetFreePort((new Random()).Next(12000, 13000));
      this.port = new ForwardedPortLocal("localhost", Convert.ToUInt32(portNumber), endpoint.internalIp, 3389);
      client.AddForwardedPort(port);

      port.Start();

      CredentialsTool.DeleteCredentials("TERMSRV/localhost");
      CredentialsTool.DeleteCredentials("localhost");

      CredentialsTool.AddCredential("TERMSRV/localhost", endpoint.credential.name, endpoint.credential.plainPassword);

      String mstscCmd = $"/v:localhost:{portNumber}";
      ProcessStartInfo mstscInfo = new ProcessStartInfo("mstsc.exe", mstscCmd);
      this.rdcProcess = new Process();
      this.rdcProcess.StartInfo = mstscInfo;
      rdcThread = ProcessWatcher.WatchForExit(this.rdcProcess);
    }
  }
}
