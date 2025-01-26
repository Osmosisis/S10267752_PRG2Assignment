using System;
using System.Collections.Generic;
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

		private bool supportcfft;

		public bool SupportCFFT
		{
			get { return supportcfft; }
			set { supportcfft = value; }
		}
		private bool supportddjb;

		public bool SupportDDJB
		{
			get { return supportddjb; }
			set { supportddjb = value; }
		}
		private bool supportlwtt;

		public bool SupportLWTT
		{
			get { return supportlwtt; }
			set { supportlwtt = value; }
		}


		public BoardingGate() { }
		public BoardingGate(string gatename, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
		{
			GateName = gatename;
            SupportCFFT = supportsCFFT;
            SupportDDJB = supportsDDJB;
            SupportLWTT = supportsLWTT;
            Flight = null;
        }
        public double CalculateFees(Flight flight)
        {
            double totalFee = 300; 

            if (flight is DDJBFlight && SupportDDJB)
                totalFee += 300; 
            else if (flight is CFFTFlight && SupportCFFT)
                totalFee += 150; 
            else if (flight is LWTTFlight && SupportLWTT)
                totalFee += 500;

            return totalFee;
        }

        public override string ToString()
        {
            return $"Gate Name: {GateName} \tSupports CFFT: {SupportCFFT} \tSupports DDJB:{SupportDDJB} \tSupports LWTT: {SupportLWTT} \tFlight:{Flight}";
        }
    }
}
