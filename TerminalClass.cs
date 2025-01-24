using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    internal class Terminal
    {
        private string terminalname;

        public string TerminalName
        {
            get { return terminalname; }
            set { terminalname = value; }
        }

        public Dictionary<string, Flight> flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, Airline> airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, BoardingGate> boardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();


        public Terminal() { }
        public Terminal(string terminalname)
        {
            TerminalName = terminalname;
        }
        public bool AddAirline(Airline airline)
        {

        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {

        }
        public void PrintAirlineFees()
        {

        }
        public Airline GetAirlineFromFlight(Flight flight)
        {
            Dictionary<string, string> airlineread = new Dictionary<string, string>();
            string[] a = File.ReadAllLines("airlines.csv");
            foreach (string s in a)
            {
                airlineread[s.Split(",")[0]] = s.Split(",")[1];
            }
            string airpre = flight.FlightNum.Substring(0, 2);
            foreach (string s in airlineread.Keys)
            {
                if (airpre == airlineread[s])
                {
                    string airname = s;
                    foreach (string w in airlines.Keys)
                    {
                        if (airname == w)
                        {
                            return airlines[w];
                        }
                    }
                }
            }
            return null;
        }
        public override string ToString()
        {
            return $"Terminal Name: {TerminalName}";
        }
    }
}
