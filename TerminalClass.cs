using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================


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
            if (airline == null)
            {
                throw new ArgumentNullException(nameof(airline), "Airline cannot be null.");
            }
            if (!airlines.ContainsKey(airline.Name))
            {
                airlines.Add(airline.Name, airline);
                return true;
            }
            return false;
        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (boardingGate == null)
            {
                throw new ArgumentNullException(nameof(boardingGate), "Airline cannot be null.");
            }
            if (!airlines.ContainsKey(boardingGate.GateName))
            {
                boardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
            return false;
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
            string airpre = flight.FlightNumber.Substring(0, 2);
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
