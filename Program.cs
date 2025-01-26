using S10267752_PRGassignment2;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================

Dictionary<string, Airline> newair = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> newboard = new Dictionary<string, BoardingGate>();
void initAirlines()
{
    string[] a = File.ReadAllLines("airlines.csv");
    string[] header = a[0].Split(",");
    foreach (string s in a)
    {
        string[] t = s.Split(',');
        Airline airinit = new Airline(t[0], t[1]);
        newair.Add(t[0], airinit);
    }
}
void initBoardingGates()
{
    string[] a = File.ReadAllLines("boardinggates.csv");
    foreach (string line in a.Skip(1)) 
    {
        string[] parts = line.Split(',');
        // Parse and validate the data
        string gateName = parts[0];
        bool supportsCFFT = bool.TryParse(parts[1], out bool cfft) && cfft;
        bool supportsDDJB = bool.TryParse(parts[2], out bool ddjb) && ddjb;
        bool supportsLWTT = bool.TryParse(parts[3], out bool lwtt) && lwtt;

        // Create the BoardingGate object
        BoardingGate gate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);

        // Add to the newboard dictionary
        newboard.Add(gateName, gate);
    }
}
initAirlines();
initBoardingGates();
