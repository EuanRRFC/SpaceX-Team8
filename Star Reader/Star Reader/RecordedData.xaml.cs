using System;
using System.Collections.Generic;
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
            someotherfunction(x);
        }


        public void someotherfunction(int x)
        {
            Recording R = (Recording)App.RecordStore[x];
            if (R != null)
            {
                int length = R.getNumberOfPackets();
                int Size = 20;
                for (int i = 0; i < length; i++)
                {
                    Packet p = R.getPacket(i);
                    Button btn1 = new Button();
                    btn1.Width = Size;
                    btn1.Height = Size;
                    if (p.getPacketType() == 'E')
                    {
                        btn1.Background = Brushes.Red;
                        btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getErrorType();
                    }
                    else
                    {
                        btn1.Background = Brushes.Blue;
                        btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                    }
                    PacketViewerA.Children.Add(btn1);
                    Button btn2 = new Button();
                    btn2.Height = Size;
                    if (p.getPacketType() == 'E')
                    {
                        btn2.Background = Brushes.Red;
                        btn2.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getErrorType();
                    }
                    else
                    {
                        btn2.Background = Brushes.Blue;
                        btn2.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                    }
                    DetailedViewerA.Children.Add(btn2);
                }
            }
        }

    }
}
