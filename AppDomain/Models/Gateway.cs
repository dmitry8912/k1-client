using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K1_Insight.AppDomain.Models;
namespace K1_Insight.AppDomain.Models
{
  public class Gateway
  {
    public Guid id;
    public string url;
    public Guid credentialId;
    public string note;
    public DateTime created_at;
    public DateTime updated_at;
    public Credential credential;
  }
}
