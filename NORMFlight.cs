using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267752_PRGassignment2
{
    internal class NORMFlight : Flight 
    {
        public NORMFlight() { }
        public NORMFlight(string fnum, string og, string dest, DateTime et, string stat) : base( fnum,  og,  dest,  et,  stat) { }
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
