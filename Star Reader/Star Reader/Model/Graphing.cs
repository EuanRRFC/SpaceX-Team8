﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Reader.Model
{

    class Graphing
    {

        public List<double> getPlots(Recording r)
        {
            int dataRatePerMinute = 0;
            List<double> plot = new List<double>();
            Packet currentPacket = r.ListOfPackets[0];
            DateTime interval = currentPacket.Time;
            Packet end = r.ListOfPackets[r.ListOfPackets.Count - 1];
            int y = 1;

            for (int x = 0; x < r.ListOfPackets.Count - 1; x++)
            {
                if (interval >= end.Time) { break; }
                do
                {
                    if (currentPacket.Payload != null)
                    {
                        dataRatePerMinute += currentPacket.Payload.Length;
                    }
                    currentPacket = r.ListOfPackets[y];
                    y++;
                } while (currentPacket.Time <= interval);

                plot.Add(dataRatePerMinute);
                interval = interval.AddMinutes(1);
                dataRatePerMinute = 0;
            }
            return plot;
        }



        public List<double> getBars(Recording r)
        {
            List<double> barsDcPaEeEr = new List<double>();
            Packet currentPacket;
            int packetCount = r.ListOfPackets.Count;
            int DC = 0;
            int Parity = 0;
            int EEPs = 0;
            int Errors = 0;
            for (int x = 0; x < packetCount; x++)
            {
                currentPacket = r.ListOfPackets[x];
                switch (currentPacket.ErrorType)
                {
                    case "Disconnect":
                        DC++;
                        Errors++;
                        break;
                    case "Parity":
                        Parity++;
                        Errors++;
                        break;
                    case "EEP":
                        EEPs++;
                        Errors++;
                        break;
                    case "Error":
                        Errors++;
                        break;
                    default:
                        break;

                }

                switch (currentPacket.PacketType.ToString())
                {
                    case "Disconnect":
                        DC++;
                        Errors++;
                        break;
                    case "Parity":
                        Parity++;
                        Errors++;
                        break;
                    case "EEP":
                        EEPs++;
                        Errors++;
                        break;
                    case "Error":
                        Errors++;
                        break;
                    default:
                        break;

                }
            }
            barsDcPaEeEr.Add(DC);
            barsDcPaEeEr.Add(Parity);
            barsDcPaEeEr.Add(EEPs);
            barsDcPaEeEr.Add(Errors);
            return barsDcPaEeEr;

        }
    }
}