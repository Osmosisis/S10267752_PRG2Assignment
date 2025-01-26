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
    internal class Airline
    {
		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private string code;

		public string Code
		{
			get { return code; }
			set { code = value; }
		}

		public Dictionary<string, Flight> flights { get; set; } = new Dictionary<string, Flight>();

		public Airline(string name, string code, Dictionary<string, Flight> f)
		{
			Name = name;
			Code = code;

		}
        public double CalculateFees()
        {
            return 1.0;
        }
    }
}
