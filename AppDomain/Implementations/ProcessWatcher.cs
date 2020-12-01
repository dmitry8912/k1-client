using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K1_Insight.AppDomain.Implementations
{
  public class ProcessWatcher
  {
    public static Thread WatchForExit(Process p)
    {
      Thread t = new Thread(delegate ()
      {
        try
        {
          p.Start();
          p.WaitForExit();
        } catch (Exception ex)
        {
          
        };
      });
      t.Start();
      return t;
    }

    public static Thread ExtendLock(Guid lockId)
    {
      Thread t = new Thread(delegate ()
      {
        try
        {
          while(true)
          {
            Backend.ExtendLock(lockId);
            Thread.Sleep(30000);
          } 
        }
        catch (Exception ex)
        {

        };
      });
      t.Start();
      return t;
    }
  }
}
