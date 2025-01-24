using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    internal class BoardingGate
    {
		private string gatename;

		public string GateName
		{
			get { return gatename; }
			set { gatename = value; }
		}

		private Flight flight;

		public Flight Flight
		{
			get { return flight; }
			set { flight = value; }
		}

		public BoardingGate() { }
		public BoardingGate(string gatename, Flight f)
		{
			GateName = gatename;
			Flight = f;
		}
        public double CalculateFees()
        {
            return 1.0;
        }
    }
}
