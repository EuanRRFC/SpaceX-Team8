using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Star_Reader.Model
{
    class FileReader
    {

        public FileReader()
        {

        }

        public void storeRecording(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            Recording r = new Recording();
            r.addStartTime(Convert.ToDateTime(lines[0]));
            r.addChannel(Int32.Parse(lines[1]));
            r.addEndTime(Convert.ToDateTime(lines[lines.Length - 1]));

            int jump = 5;
            for (int i = 3; i < lines.Length - 3; i += jump)
            {
                DateTime dt = Convert.ToDateTime(lines[i]);
                char pt = Convert.ToChar(lines[i + 1]);
                Packet p;
                if (pt == 'P')
                {
                    string payload = lines[i + 2];
                    string message = lines[i + 3];
                    string packetend = lines[i + 3];
                    p = new Packet(dt, payload, pt, packetend);
                    r.addPacket(p);
                    jump = 5;
                }
                else if (pt == 'E')
                {
                    string ErrorType = lines[i + 2];
                    p = new Packet(dt, pt, ErrorType);
                    r.addPacket(p);
                    jump = 4;
                }

            }
            App.RecordStore[r.getChannel()] = r;
        }
    }
}
