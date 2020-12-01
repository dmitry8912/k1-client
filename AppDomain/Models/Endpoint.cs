using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Insight.AppDomain.Models
{
  public class Endpoint
  {
    public Guid id;
    public string internalIp;
    public Guid credentialId;
    public Guid gatewayId;
    public string note;
    public DateTime created_at;
    public DateTime updated_at;
    public Credential credential;
    public Gateway gateway;
  }
}
