using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Star_Reader.Model;
using LiveCharts;
using System.ComponentModel;
using System.Globalization;

namespace Star_Reader
{
    /// <summary>
    /// Interaction logic for RecordedData.xaml
    /// </summary>
    public partial class DetailsTab : TabItem, INotifyPropertyChanged
    {
        public ChartValues<double> Values1 { get; set; }

        private ICollectionView dataGridCollection;
        private string filterString;

        public ICollectionView DataGridCollection
        {
            get { return dataGridCollection; }
            set { dataGridCollection = value; NotifyPropertyChanged("DataGridCollection"); }
        } 
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public string FilterString
        {
            get { return filterString; }
            set
            {
                filterString = value;
                NotifyPropertyChanged("FilterString");
                FilterCollection();
            }
        }

        private void FilterCollection()
        {
            dataGridCollection?.Refresh();
        }

        public bool Filter(object obj)
        {
            var packet = obj as Packet;
            if (packet == null ) return false;
            if (string.IsNullOrEmpty(filterString)) return true;
            return packet.ErrorType != null && CultureInfo.CurrentCulture.CompareInfo.IndexOf(packet.ErrorType, filterString, CompareOptions.IgnoreCase) >= 0;
        }

        public DetailsTab(int portNr)
        {
            InitializeComponent();
            PopulateOverview(portNr);
            DataGridCollection = CollectionViewSource.GetDefaultView(App.RecordingData[portNr].ListOfPackets);
            DataGridCollection.Filter = Filter;
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
                        Button btn1s = new Button
                        {
                            Width = size,
                            Height = size
                        };

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
                    switch (p.ErrorType)
                    {
                        case "Disconnect":
                            btn1.Background = Brushes.Red;
                            btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.ErrorType;
                            btn1.Content = p.ErrorType[0];
                            break;
                        case "Parity":
                            btn1.Background = Brushes.Yellow;
                            btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.ErrorType;
                            btn1.Content = p.ErrorType[0];
                            break;
                    }
                }
                else
                {
                    if (p.PacketEnd.Equals("EOP"))
                    {
                        btn1.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#00dddd");
                        btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.Payload + "\n" + p.PacketEnd;
                    }
                    else
                    {
                        btn1.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc");
                        btn1.ToolTip = p.Time + "." + p.Time.ToString("fff") + "\n" + p.PacketType + "\n" + p.Payload + "\n" + p.PacketEnd;
                    }
                }
                btn1.Click += new RoutedEventHandler(btn_click);
                btn1.Tag = portNr +""+ i;
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
            int port = int.Parse(portc+"");
            int item = int.Parse(x.Substring(1));
            DetailedViewerA.ScrollIntoView(App.RecordingData[port].ListOfPackets[item]);
            DetailedViewerA.SelectedItem=App.RecordingData[port].ListOfPackets[item];
        }
    }
}