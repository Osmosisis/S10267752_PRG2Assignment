using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    internal class TerminalClass
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


        public TerminalClass() { }
        public TerminalClass(string terminalname)
        {
            TerminalName = terminalname;
        }
    }
}
