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
    public interface ILWTTFlight
    {
        public double CalculateFees();
    }
    internal class LWTTFlight : Flight, ILWTTFlight
    {
        private double requestfee = 500;

        public double RequestFee
        {
            get { return requestfee; }
            set { requestfee = value; }
        }


        public LWTTFlight() { }
        public LWTTFlight(string fnum, string og, string dest, DateTime et) : base( fnum,  og,  dest,  et) { }
        public override double CalculateFees()
        {
            return requestfee;
            //the subclasses only return the special request fee, the actual total fee is returned by the flight base class.

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
