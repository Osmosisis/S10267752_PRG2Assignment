using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================

namespace S10267752_PRGassignment2
{
    public interface INORMFlight
    {
        public double CalculateFees();
    }
    internal class NORMFlight : Flight, INORMFlight
    {
        public NORMFlight() { }
        public NORMFlight(string fnum, string og, string dest, DateTime et) : base( fnum,  og,  dest,  et) { }
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
