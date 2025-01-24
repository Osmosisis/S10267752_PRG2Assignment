using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    internal class DDJBFlight : Flight
    {
        private double requestfee;

        public double RequestFee
        {
            get { return requestfee; }
            set { requestfee = value; }
        }
        public DDJBFlight() { }
        public DDJBFlight(string fnum, string og, string dest, DateTime et, string stat, double requestfee) : base(fnum, og, dest, et, stat)
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
