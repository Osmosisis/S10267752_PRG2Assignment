using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================

namespace S10267752_PRGassignment2
{
    internal class Airline
    {
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string code;

		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		private Dictionary<string, Flight> flights;
		
		public Dictionary<string, Flight> Flights
		{
			get {return flights;}
			set {flights = value;}
		}
		public Airline(string aname, string acode)
		{
			name = aname;
			code = acode;
			flights = new Dictionary<string, Flight>();
		}

		public bool AddFlight(Flight flight)
		{	
			if (flights.ContainsKey(flight.FlightNumber) == false)
			{
				flights.Add(flight.FlightNumber, flight);
				return true;
			}
			else
			{
				return false;
			}
			
		}



		public bool RemoveFlight(Flight flight)
		{	
			if (flights.ContainsKey(flight.FlightNumber) == true)
			{
				flights.Remove(flight.FlightNumber);
				return true;
			}
			else
			{
				return false;
			}
			
		}

        public double CalculateFees()
        {
			// For every 3 flights arriving/departing, airlines will receive a discount	$350
			// For each flight arriving/departing before 11am or after 9pm	$110
			// For each flight with the Origin of Dubai (DXB), Bangkok (BKK) or Tokyo (NRT)	$25
			// For each flight not indicating any Special Request Codes	$50
			// For each airline with more than 5 flights arriving/departing, the airline will receive an additional discount	3% off the Total Bill (before any other Discounts)

			double total = 0;
			const int threeflight = 350;
			const int lateflight = 110;
			const int specificorigin = 25;
			const int nospecialreq = 50;
			const double morethanfiveflight = 0.03;




			foreach (KeyValuePair<string, Flight> pair in flights)
			{
				Flight f = pair.Value;
				total += f.CalculateFees();
			}
			//discounts

			//check for >5 flights
			if (flights.Count() > 5)
			{
				total -= total*morethanfiveflight;
			}


			//every 3 flights
			double discount = 0;
			discount += threeflight*Convert.ToInt64(flights.Count()/3);

			//late flights, special origin, no special req

			TimeSpan elevenAM = new TimeSpan(11, 0, 0);
			TimeSpan ninePM = new TimeSpan(21, 0, 0);
			
			foreach (KeyValuePair<string, Flight> pair in flights)
			{
				Flight f = pair.Value;

				//late flight
				if (f.ExpectedTime.TimeOfDay < elevenAM || f.ExpectedTime.TimeOfDay > ninePM)
				{
					discount += lateflight;
				}

				//specific origin
				if (f.Origin == "Dubai (DXB)" || f.Origin == "Bangkok (BKK)" || f.Origin == "Tokyo (NRT)")
				{
					discount += specificorigin;
				}

				//no special req
				if (this is NORMFlight)
				{
					discount += nospecialreq;
				}
			}
			total -= discount;

            return total;
        }

		public static bool IsValidCode(string s)
		{
			return (s.Length == 2) && s.All(char.IsLetter) && s.All(char.IsUpper);
		}

		public override string ToString()
		{
			string ret = null;
			foreach (KeyValuePair<string, Flight> pair in flights)
			{
				ret += $"Flight Number: {pair.Value.FlightNumber}\nAirline Name: {name}\nOrigin: {pair.Value.Origin}\nDestination: {pair.Value.Destination}\nExpected Departure/Arrival: {pair.Value.ExpectedTime}\n\n";
			}
			ret += "\n";
			return ret;
		}


    }
}
