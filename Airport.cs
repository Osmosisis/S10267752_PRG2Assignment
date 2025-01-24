﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    abstract class Flight
    {
		private string flightnum;

		public string FlightNum
		{
			get { return flightnum; }
			set { flightnum = value; }
		}

		private string origin;

		public string Origin
		{
			get { return origin; }
			set { origin = value; }
		}

		private string destination;

		public string Destination
		{
			get { return destination; }
			set { destination = value; }
		}

		private DateTime expectedtime;

		public DateTime ExpectedTime
		{
			get { return expectedtime; }
			set { expectedtime = value; }
		}

		private string status;

		public string Status
		{
			get { return status; }
			set { status = value; }
		}

		public Flight() { }
		public Flight(string fnum, string og, string dest, DateTime et, string stat)
		{
			FlightNum = fnum;
			Origin = og;
			Destination = dest;
			ExpectedTime = et;
			Status = stat;
		}
        public virtual double CalculateFees();

		public virtual string ToString()
		{
			return $"Flight Number: {FlightNum} \tOrigin: {Origin} \tDestination: {Destination} \tExpected Time: {ExpectedTime} \tStatus: {Status}";
		}
    }
}
