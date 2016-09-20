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

namespace Star_Reader
{
    /// <summary>
    /// Interaction logic for RecordedData.xaml
    /// </summary>
    public partial class RecordedData : Window
    {


        public RecordedData(int x)
        {
            InitializeComponent();
            PopulateOverview(x);
            PopulateDataGrid(x);
        }

        public void PopulateDataGrid(int x)
        {
            Recording r = (Recording)App.RecordStore[x];
            DetailedViewerA.ItemsSource = r.ListOfPackets;
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

        public void PopulateOverview(int x)
        {
            const int size = 20;
            Recording r = (Recording)App.RecordStore[x];
            if (r != null)
            {
                int length = r.ListOfPackets.Count;

                for (int i = 0; i < length; i++)
                {
                    Packet p = r.ListOfPackets[i];
                    Button btn1 = new Button
                    {
                        Width = size,
                        Height = size
                    };
                    if (p.getPacketType() == 'E')
                    {
                        btn1.Background = Brushes.Red;
                        btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getErrorType();
                        btn1.Content = p.getErrorType()[0];

                    }
                    else
                    {
                        if (p.getPacketEnd().Equals("EOP"))
                        {
                            btn1.Background = Brushes.Blue;
                            btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() +
                                           "\n" + p.getPacketEnd();
                        }
                        else
                        {
                            btn1.Background = Brushes.Orange;
                            btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() +
                                           "\n" + p.getPacketEnd();
                        }
                    }
                    PacketViewerA.Children.Add(btn1);

                }
            }

            Recording rb = (Recording)App.RecordStore[x + 1];
            if (rb == null) return;
            {
                int length = rb.ListOfPackets.Count;
                for (int i = 0; i < length; i++)
                {
                    Packet p = rb.ListOfPackets[i];
                    Button btn3 = new Button
                    {
                        Width = size,
                        Height = size
                    };
                    Button btn4 = new Button();
                    btn4.Height = size;
                    if (p.getPacketType() == 'E')
                    {
                        btn3.Background = Brushes.Red;
                        btn3.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getErrorType();
                        btn3.Content = p.getErrorType()[0];
                        btn4.Background = Brushes.Red;
                        btn4.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getErrorType();
                    }
                    else
                    {
                        if (p.getPacketEnd().Equals("EOP"))
                        {
                            btn3.Background = Brushes.Blue;
                            btn3.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                            btn4.Background = Brushes.Blue;
                            btn4.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getPacketData() + " - " + p.getPacketEnd();
                            btn4.Foreground = Brushes.White;
                        }
                        else
                        {
                            btn3.Background = Brushes.Orange;
                            btn3.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                            btn4.Background = Brushes.Orange;
                            btn4.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getPacketData() + " - " + p.getPacketEnd();
                        }
                    }
                    PacketViewerB.Children.Add(btn3);
                    DetailedViewerB.Children.Add(btn4);
                }
            }
        }
    }
}