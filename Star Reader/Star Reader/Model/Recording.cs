using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Reader.Model
{
    class Recording
    {
        private List<Packet> listOfPackets = new List<Packet>();
        private DateTime packetStartTime;
        private DateTime packetEndTime;
        private int channel;
        private int errorsPresent = 0;

        public Recording()
        {

        }

        public void addStartTime(DateTime start)
        {
            packetStartTime = start;
        }
        public void addEndTime(DateTime end)
        {
            packetEndTime = end;
        }
        public void addChannel(int c)
        {
            channel = c;
        }
        public void addPacket(Packet toAdd)
        {
            listOfPackets.Add(toAdd);
            if (toAdd.getPacketType() == 'E')
            {
                errorsPresent++;
            }
        }

        public int getNumberOfPackets()
        {
            return listOfPackets.Count;
        }
        public int getNumberOfErrors()
        {
            return errorsPresent;
        }
        public Packet getPacket(int x)
        {
            return listOfPackets[x];
        }
        public DateTime getStartTime()
        {
            return packetStartTime;
        }
        public DateTime getEndTime()
        {
            return packetEndTime;
        }
        public int getChannel()
        {
            return channel;
        }
    }
}
