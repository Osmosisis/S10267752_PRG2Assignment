﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace S10267752_PRGassignment2
{
    abstract class Flight : IComparable<Flight>
    {
        private string flightNumber;

        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
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

        private DateTime expectedTime;

        public DateTime ExpectedTime
        {
            get { return expectedTime; }
            set { expectedTime = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public Flight() { }
        public Flight(string fnum, string og, string dest, DateTime et)
        {
            FlightNumber = fnum;
            Origin = og;
            Destination = dest;
            ExpectedTime = et;
            Status = "Scheduled";
        }


        public int CompareTo(Flight f)
        {
            return expectedTime.CompareTo(f.expectedTime);
        }

        public BoardingGate GetBoardingGate(Terminal t)
        {
            BoardingGate gate = null;
            foreach (KeyValuePair<string, BoardingGate> g in t.BoardingGates)
            {
                if (g.Value.Flight != null && g.Value.Flight.FlightNumber == flightNumber)
                {
                    gate = g.Value;
                    break;
                }
            }
            return gate;
        }

        public static bool IsValidCode(string s)
        {
            return Regex.IsMatch(s, @"^[A-Z]{2} \d{3}$");
        } 


// Arriving Flight	$500	Arriving Flights are Flights with Destination as Singapore (SIN)
// Departing Flight	$800	Departing Flights are Flights with Origin as Singapore (SIN)
// Boarding Gate Base Fee	$300	Base Fee for All Boarding Gates


        public virtual double CalculateFees()
        {
            double cost = 0;
            int af = 500;
            int df = 800;
            int bg = 300;
            if (origin == "Singapore (SIN)")
            {
                cost += df;
            }
            else
            {
                cost += af;
            }

            //assuming every flight must have a bording gate:

            cost += bg;
            if (this is LWTTFlight)
            {
                LWTTFlight lwtt = (LWTTFlight)this;
                cost += lwtt.CalculateFees();
            }
            if (this is DDJBFlight)
            {
                DDJBFlight ddjb = (DDJBFlight)this;
                cost += ddjb.CalculateFees();
            }
            if (this is CFFTFlight)
            {
                CFFTFlight cfft = (CFFTFlight)this;
                cost += cfft.CalculateFees();
            }
            return cost;
        }

        public virtual string ToString()
        {   
            string src = "";
            if (this is DDJBFlight)
            {
                src = "DDJB";
            }
            else if (this is LWTTFlight)
            {
                src = "LWTT";
            }
            else if (this is CFFTFlight)
            {
                src = "CFFT";
            }
            else
            {
                src = "None";
            }
            return $"Flight Number: {flightNumber} \nOrigin: {origin} \nDestination: {destination} \nExpected Time: {expectedTime} \nSpecial Request Code: {src}\n";
        }


    }


}
