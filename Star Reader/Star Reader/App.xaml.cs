using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Star_Reader.Model;

namespace Star_Reader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       public static Dictionary<int,Recording> RecordingData= new Dictionary<int, Recording>();
    }
}
