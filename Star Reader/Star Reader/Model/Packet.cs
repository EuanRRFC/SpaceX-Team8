using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Reader.Model
{
    class Packet
    {
        private DateTime time;
        private string payload;
        private char packetType;
        private string errorType;
        private string packetEnd;

        public Packet(DateTime dt, string contents, char pt, string pe)
        {
            time = dt;
            payload = contents;
            packetType = pt;
            packetEnd = pe;
        }
        public Packet(DateTime dt, char pt, string et)
        {
            time = dt;
            packetType = pt;
            errorType = et;
        }

        public char getPacketType()
        {
            return packetType;
        }
        public DateTime getPacketTime()
        {
            return time;
        }
        public string getPacketData()
        {
            return payload;
        }
        public string getErrorType()
        {
            return errorType;
        }
        public string getPacketEnd()
        {
            return packetEnd;
        }

    }
}
