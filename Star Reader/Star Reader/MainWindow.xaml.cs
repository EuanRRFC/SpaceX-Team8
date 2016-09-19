using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Star_Reader.Model;

namespace Star_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string tag = b.Tag.ToString();
            FileReader fr = new FileReader();
            fr.storeRecording(tag);

        }

        private void ButtonShowFile_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            RecordedData r = new RecordedData(Int32.Parse(b.Tag.ToString()));
            r.Show();
        }
    }
}
