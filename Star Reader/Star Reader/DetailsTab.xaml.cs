using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Star_Reader.Model;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using Color = System.Drawing.Color;
using LiveCharts;

namespace Star_Reader
{
    /// <summary>
    /// Interaction logic for RecordedData.xaml
    /// </summary>
    public partial class DetailsTab : TabItem
    {


        public ChartValues<double> Values1 { get; set; }

        public DetailsTab(int portNr)
        {
            InitializeComponent();
            PopulateOverview(portNr);
            PopulateDataGrid(portNr);
        }

        public void Refresh(int portNr)
        {
          
        }

        public void PopulateDataGrid(int portNr)
        {
            DetailedViewerA.ItemsSource = App.RecordingData[portNr].ListOfPackets;
            //foreach (DataGridRow row in DetailedViewerA.ItemContainerGenerator.Items)
            //{
            //    row.Background = Brushes.Red;
            //    //switch ((string)row.Item["ErrorType"].Value)
            //    //{
            //    //    case "E":
                       
            //    //        break;
            //    //    case "EOP":
            //    //        row.Background = Brushes.Blue;
            //    //        row.Foreground = Brushes.White;
            //    //        break;
            //    //    default:
            //    //        row.Background = Brushes.Orange;
            //    //        break;
            //    //}
            //}
        }

        public void PopulateOverview(int portNr)
        {
            const int size = 20;
            var r = App.RecordingData[portNr];
            if (r == null) return;
            int length = r.ListOfPackets.Count;

            for (int i = 0; i < length; i++)
            {
                Packet p = r.ListOfPackets[i];
                if(i>0)
                {
                    Packet NextP = r.ListOfPackets[i - 1];
                    TimeSpan td = p.Time.Subtract(NextP.Time);
                    if(td.TotalMilliseconds > 100)
                    {
                        Button btn1s = new Button();
                        btn1s.Width = size;
                        btn1s.Height = size;

                        switch (td.Seconds)
                        {
                            case 0:
                                btn1s.ToolTip = "Empty Space of 0." + td.TotalMilliseconds + " seconds.";
                                btn1s.Background = Brushes.White;
                                break;
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                btn1s.ToolTip = "Empty Space of " + td.Seconds + "." + td.TotalMilliseconds.ToString().Substring(1) + " seconds.";
                                btn1s.Background = Brushes.Beige;
                                break;
                            default:
                                btn1s.ToolTip = "Empty Space of " + td.Seconds + "." + td.TotalMilliseconds.ToString().Substring(1) + " seconds.";
                                btn1s.Background = Brushes.Crimson;
                                break;
                        }
                        PacketViewerA.Children.Add(btn1s);
                        
                    }
                }
                Button btn1 = new Button
                {
                    Width = size,
                    Height = size
                };
                if (p.PacketType == 'E')
                {
                    btn1.Background = Brushes.Red;
                    btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.ErrorType;
                    btn1.Content = p.ErrorType[0];
                }
                else
                {
                    if (p.PacketEnd.Equals("EOP"))
                    {
                        btn1.Background = Brushes.Blue;
                        btn1.ToolTip = p.Time +"."+p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.Payload +"\n" + p.PacketEnd;
                    }
                    else
                    {
                        btn1.Background = Brushes.Orange;
                        btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType+ "\n" + p.Payload +"\n" + p.PacketEnd;
                    }
                }
                btn1.Click += new RoutedEventHandler(btn_click);
                btn1.Tag = (portNr +""+ i);
                PacketViewerA.Children.Add(btn1);
            }
            Values1 = new ChartValues<double> { 3, 4, 6, 3, 2, 6 };
            DataContext = this;
        }
        protected void btn_click(object sender, EventArgs e)
        {
            Button b = (Button) sender;
            string x = b.Tag.ToString();
            char portc = x[0];
            int port = Int32.Parse(portc+"");
            int item = Int32.Parse(x.Substring(1));
            DetailedViewerA.ScrollIntoView(App.RecordingData[port].ListOfPackets[item]);

            
        }
    }
}