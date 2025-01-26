using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    public interface ICFFTFlight
    {
        public double CalculateFees();
    }
    internal class CFFTFlight : Flight, ICFFTFlight
    {
		private double requestfee = 150;

		public double RequestFee
		{
			get { return requestfee; }
			set { requestfee = value; }
		}
        
        public override double CalculateFees()
        {
            return requestfee;
            //the subclasses only return the special request fee, the actual total fee is returned by the flight base class.
        }
        
        public override string ToString()
        {
            return $"{base.ToString()} \n${requestfee} requested (CFFT)";
        }
    }
}
