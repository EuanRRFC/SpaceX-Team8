using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Reader.Model
{
    public class Packet
    {
        public DateTime Time { get; set; }
        public string Payload { get; set; }
        public char PacketType { get; set; }
        public string ErrorType { get; set; }
        public string PacketEnd { get; set; }

        public Packet(DateTime dt, string contents, char pt, string pe)
        {
            Time = dt;
            Payload = contents;
            PacketType = pt;
            PacketEnd = pe;
        }
        public Packet(DateTime dt, char pt, string et)
        {
            Time = dt;
            PacketType = pt;
            ErrorType = et;
        }

        public char getPacketType()
        {
            return PacketType;
        }
        public DateTime getPacketTime()
        {
            return Time;
        }
        public string getPacketData()
        {
            return Payload;
        }
        public string getErrorType()
        {
            return ErrorType;
        }
        public string getPacketEnd()
        {
            return PacketEnd;
        }

    }
}
