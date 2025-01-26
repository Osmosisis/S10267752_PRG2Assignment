using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{   
    public interface IDDJBFlight
    {
        public double CalculateFees();
    }
    internal class DDJBFlight : Flight, IDDJBFlight
    {
        private double requestfee = 300;

        public double RequestFee
        {
            get { return requestfee; }
            set { requestfee = value; }
        }

        public override double CalculateFees()
        {
            return requestfee;
        }
        public override string ToString()
        {
            return $"{base.ToString()} \n${requestfee} requested (DDJB)";
        }
    }
}
