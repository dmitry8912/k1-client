using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_Insight.AppDomain.Models
{
  public class Credential
  {
    public Guid id;
    public string name;
    public string plainPassword;
    public string note;
    public DateTime created_at;
    public DateTime updated_at;
  }
}
