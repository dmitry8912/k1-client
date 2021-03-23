using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using K1_Insight.AppDomain.Models;
using System.Net;
using System.Configuration;
namespace K1_Insight.AppDomain.Implementations
{
  public class Backend
  {
    public static Endpoint GetLockInfo(Guid lockId)
    {
      
      using (var client = new HttpClient())
      {
        var url = String.Format("{0}/endpoints/direct/info/{1}", ConfigurationSettings.AppSettings["url"], lockId.ToString());

        var response = client.GetAsync(String.Format("{0}/endpoints/direct/info/{1}", ConfigurationSettings.AppSettings["url"], lockId.ToString())).Result;
        if (response.IsSuccessStatusCode)
        {
          return JsonConvert.DeserializeObject<Endpoint>(response.Content.ReadAsStringAsync().Result);
        }
        else
        {
          if (response.Content.ReadAsStringAsync().Result != String.Empty && response.StatusCode == HttpStatusCode.BadRequest)
          {
            var error = JsonConvert.DeserializeObject<LockError>(response.Content.ReadAsStringAsync().Result);
            throw new Exception(error.error);
          }
          else
          {
            throw new Exception("При выполнении запроса произошла ошибка");
          }
        }
      }
    }

    public static Endpoint GetBookInfo(Guid bookId)
    {
      throw new NotImplementedException();
    }

    public static void ExtendLock(Guid lockId)
    {
      using (var client = new HttpClient())
      {
        var response = client.GetAsync(String.Format("{0}/endpoints/direct/extend/{1}", ConfigurationSettings.AppSettings["url"], lockId.ToString())).Result;
        if (!response.IsSuccessStatusCode)
        {
          throw new Exception("Extending lock error");
        }
      }
    }
  }
}
