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
    internal class CFFTFlight : Flight
    {
		private double requestfee;

		public double RequestFee
		{
			get { return requestfee; }
			set { requestfee = value; }
		}
		public CFFTFlight() { }
		public CFFTFlight(string fnum, string og, string dest, DateTime et, string stat, double requestfee) : base(fnum, og, dest, et, stat) 
		{
			RequestFee = requestfee;
		}
        public override double CalculateFees()
        {
            return base.CalculateFees();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
