using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
