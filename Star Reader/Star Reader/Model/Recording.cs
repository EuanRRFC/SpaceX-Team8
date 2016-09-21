using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using System.Text;
using System.Threading.Tasks;

namespace Star_Reader.Model
{
    public class Recording
    {
        public List<Packet> ListOfPackets { get; set; }
        public DateTime PacketStartTime { get; set; }
        public DateTime PacketEndTime { get; set; }
        public int Port { get; set; }
        public int ErrorsPresent { get; set; }

        public Recording()
        {
            ErrorsPresent = 0;
            ListOfPackets = new List<Packet>();
        }

        public void AddPacket(Packet toAdd)
        {
            ListOfPackets.Add(toAdd);
            if (toAdd.PacketType == 'E')
            {
                ErrorsPresent++;
            }
            
        }
        public ChartValues<double>  getDataRates()
        {
            int timeInterval = 60;
            TimeSpan RecordingLength = getDurationOfRecording();
            DateTime DataStartPoint = PacketStartTime;
            DateTime DataEndPoint = PacketStartTime.AddSeconds(timeInterval);
            int seconds = (int) Math.Round(RecordingLength.TotalSeconds / timeInterval);
            ChartValues<double> datarate = new ChartValues<double> { };
            for(int i=0;i<seconds;i++)
            {
                DataStartPoint = DataStartPoint.AddSeconds(timeInterval);
                DataEndPoint = DataEndPoint.AddSeconds(timeInterval);
                Console.WriteLine(i);
                int packets = 0;
                for(int j=0;j<ListOfPackets.Count();j++)
                {
                    TimeSpan a = ListOfPackets[j].Time.Subtract(DataStartPoint);
                    TimeSpan b = DataEndPoint.Subtract(ListOfPackets[j].Time);
                    if (ListOfPackets[j].Time.Subtract(DataStartPoint).TotalSeconds <= timeInterval && DataEndPoint.Subtract(ListOfPackets[j].Time).TotalSeconds <= timeInterval)
                        packets+= ListOfPackets[j].getNumberOfBytes();
                }
                datarate.Add(packets);
            }
            return datarate;
        }

        public int getTotalPackets()
        {
            int rate = 0;
            for(int i=0;i<ListOfPackets.Count;i++)
            {
                rate += ListOfPackets[i].getNumberOfBytes();
            }
            return rate;
        }
        //can be used to show the time the recording takes. Use .TotalSeconds or .TotalMinutes
        public TimeSpan getDurationOfRecording()
        {
            return PacketEndTime.Subtract(PacketStartTime);
        }
    }
}
