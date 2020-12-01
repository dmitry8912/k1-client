using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.NetworkInformation;
using System.Net;


using K1_Insight.AppDomain.Utils;
using K1_Insight.AppDomain.Implementations;

using K1_Insight.AppDomain.Models;

namespace K1_Insight
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    
    
    public MainWindow()
    {
      InitializeComponent();
      StatusLabel.Content = "Hello";
      RegistryTool.SetUrlHandler();
      if (Environment.GetCommandLineArgs().Count() > 1)
      {
        var paramsList = Environment.GetCommandLineArgs().Last().Replace("k1://", "").Replace("/", "").Split(':').ToList();
        if (paramsList.First() == "lock")
        {
          Endpoint lockInfo;
          try
          {
            StatusLabel.Content = String.Format("Connecting");
            lockInfo = Backend.GetLockInfo(Guid.Parse(paramsList.Last()));
            SshWrapper w = new SshWrapper(lockInfo);
            w.Connect();
            StatusLabel.Content = String.Format("Connected");
            ProcessWatcher.ExtendLock(Guid.Parse(paramsList.Last()));
          } catch (Exception ex)
          {
            StatusLabel.Content = String.Format("Error: {0}", ex.Message);
          }
        }
        else
        {
          StatusLabel.Content += "No endpoint id";
        }
      } 
      else
      {
        StatusLabel.Content = "No link";   
      };
    }
  }
}
