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
    public partial class DetailsTab : TabItem
    {

        public DetailsTab(Recording r)
        {
            InitializeComponent();
            PopulateOverview(r);
            PopulateDataGrid(r);
        }

        public void PopulateDataGrid(Recording r)
        {
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

        public void PopulateOverview(Recording r)
        {
            const int size = 20;
            if (r == null) return;
            int length = r.ListOfPackets.Count;

            for (int i = 0; i < length; i++)
            {
                Packet p = r.ListOfPackets[i];
                Button btn1 = new Button
                {
                    Width = size,
                    Height = size
                };
                if (p.PacketType == 'E')
                {
                    btn1.Background = Brushes.Red;
                    btn1.ToolTip = p.Time + "\n" + p.PacketType + "\n" + p.ErrorType;
                    btn1.Content = p.ErrorType[0];
                }
                else
                {
                    if (p.PacketEnd.Equals("EOP"))
                    {
                        btn1.Background = Brushes.Blue;
                        btn1.ToolTip = p.Time + "\n" + p.PacketType + "\n" + p.Payload +"\n" + p.PacketEnd;
                    }
                    else
                    {
                        btn1.Background = Brushes.Orange;
                        btn1.ToolTip = p.Time + "\n" + p.PacketType+ "\n" + p.Payload +"\n" + p.PacketEnd;
                    }
                }
                PacketViewerA.Children.Add(btn1);
            }
        }
    }
}