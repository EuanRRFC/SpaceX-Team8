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
                    Button btn2 = new Button();
                    btn2.Height = Size;
                    if (p.getPacketType() == 'E')
                    {
                        btn1.Background = Brushes.Red;
                        btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getErrorType();
                        btn1.Content = p.getErrorType()[0];
                        btn2.Background = Brushes.Red;
                        btn2.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getErrorType();
                    }
                    else
                    {
                        if (p.getPacketEnd().Equals("EOP"))
                        {
                            btn1.Background = Brushes.Blue;
                            btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                            btn2.Background = Brushes.Blue;
                            btn2.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getPacketData() + " - " + p.getPacketEnd();
                            btn2.Foreground = Brushes.White;
                        }
                        else
                        {
                            btn1.Background = Brushes.Orange;
                            btn1.ToolTip = p.getPacketTime() + "\n" + p.getPacketType() + "\n" + p.getPacketData() + "\n" + p.getPacketEnd();
                            btn2.Background = Brushes.Orange;
                            btn2.Content = p.getPacketTime() + " - " + p.getPacketType() + " - " + p.getPacketData() + " - " + p.getPacketEnd();
                        }
                    }
                    PacketViewerA.Children.Add(btn1);
                    DetailedViewerA.Children.Add(btn2);
                }
            }

            Recording RB = (Recording)App.RecordStore[x+1];
            if (RB != null)
            {
                int length = RB.getNumberOfPackets();
                int Size = 20;
                for (int i = 0; i < length; i++)
                {
                    Packet p = RB.getPacket(i);
                    Button btn3 = new Button();
                    btn3.Width = Size;
                    btn3.Height = Size;
                    Button btn4 = new Button();
                    btn4.Height = Size;
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