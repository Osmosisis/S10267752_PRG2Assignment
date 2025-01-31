using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using S10267752_PRGassignment2;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================

Dictionary<string, Airline> newair = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> newboard = new Dictionary<string, BoardingGate>();
// Basic Feature 1
void initAirlines()
{
    string[] a = File.ReadAllLines("airlines.csv");
    string[] header = a[0].Split(",");
    foreach (string s in a.Skip(1))
    {
        string[] t = s.Split(',');
        Airline airinit = new Airline(t[0], t[1]);
        newair.Add(t[1], airinit);  //changed t[0] to t[1]
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
// Basic Feature 2
void initFlights()
{   

    string[] a = File.ReadAllLines("flights.csv");
    string[] header = a[0].Split(",");
    foreach (string s in a.Skip(1))
    {
        Flight f = null;
        string[] t = s.TrimEnd().Split(',');

        if (t[t.Length-1] == "")
        {
            f = new NORMFlight(t[0], t[1],t[2],DateTime.Parse(t[3]));
        }
        else if (t[t.Length-1] == "DDJB")
        {
            f = new DDJBFlight(t[0], t[1],t[2],DateTime.Parse(t[3]));
        }
        else if (t[t.Length-1] == "LWTT")
        {
            f = new LWTTFlight(t[0], t[1],t[2],DateTime.Parse(t[3]));
        }
        else if (t[t.Length-1] == "CFFT")
        {
            f = new CFFTFlight(t[0], t[1],t[2],DateTime.Parse(t[3]));
        }

        string airlinecode = t[0].Substring(0,2);


        newair[airlinecode].AddFlight(f);
    
    }   
}





initAirlines();
initBoardingGates();
initFlights();
AssignBoardingGate();



// Basic Feature 3


// foreach(KeyValuePair<string,Airline> a in newair)
// {
//     System.Console.WriteLine(a.Value.ToString());
// }



/*

// Basic Feature 4
void ListBoardingGates()
{
    Console.Write(@"=============================================
List of Boarding Gates for Changi Airport Terminal 5
=============================================
");
    Console.WriteLine("Gate Name \tDDJB \tCFFT \tLWTT");
    foreach (var gate in newboard.Values)
    {
        Console.WriteLine($"{gate.GateName, -10} \t{gate.SupportDDJB} \t{gate.SupportCFFT} \t{gate.SupportLWTT}");
    }
}
*/
// Basic Feature 5
void AssignBoardingGate()
{
    Console.WriteLine("Enter Flight Number: ");
    string flightno = Console.ReadLine();
    string airlinecode = flightno.Substring(0,2);

    Airline airline = newair[airlinecode];
    Console.WriteLine(airline.flights[flightno].ToString());




    Console.WriteLine("Enter Boarding Gate Name:");
    string boardinggate = Console.ReadLine();
    while (true)
    {
        if (newboard[boardinggate].Flight == null)
        {
            break;
        }
        else
        {
            Console.Write("Boarding gate is already assigned to another flight. Please Re-Enter Boarding Gate Name: ");
        }
    }

    boardinggate.ToString();
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    string option = Console.ReadLine();
    if (option == "Y")
    {
        System.Console.WriteLine(@"1. Delayed
2. Boarding
3. On Time");
        System.Console.WriteLine("Please select the new status of the flight: ");
        option = Console.ReadLine();
        if (option == "1")
        {
            airline.flights[flightno].Status = "Delayed";
        }
        else if (option == "2")
        {
            airline.flights[airlinecode].Status = "Boarding";
        }
        else if (option == "2")
        {
            airline.flights[airlinecode].Status = "On Time";
        }


    }
    Console.WriteLine($"Flight {flightno} has been assigned to Boarding Gate {boardinggate}!");
}

/*
// Basic Feature 6

// Basic Feature 7
void ListAirlines()
{
    Console.WriteLine(@"=============================================
List of Airlines for Changi Airport Terminal 5
=============================================
");
    foreach (var air in newair.Values)
    {
        Console.WriteLine($"{air.Code, -15} {air.Name}");
    }
    Console.Write("Enter Airline Code: ");
    string aircode = Console.ReadLine();
    Console.WriteLine($"=============================================\r\nList of Flights for {newair[aircode].Name}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number", -12} {"Airline Name", -15} {"Origin", -12} {"Destination", -12} {"Expected Departure/Arrival Time"}");
}
// Basic Feature 8

// Basic Feature 9

// Main Running Code
initAirlines();
initBoardingGates();
Console.WriteLine(@"Loading Airlines...
8 Airlines Loaded!
Loading Boarding Gates...
66 Boarding Gates Loaded!
Loading Flights...
30 Flights Loaded!");
while (true)
{
    Console.Write(@"=============================================
Welcome to Changi Airport Terminal 5
=============================================
1. List All Flights
2. List Boarding Gates
3. Assign a Boarding Gate to a Flight
4. Create Flight
5. Display Airline Flights
6. Modify Flight Details
7. Display Flight Schedule
0. Exit

Please select your option: ");
    int userinput = Convert.ToInt32( Console.ReadLine());
    if (userinput == 0)
        break;
    else if (userinput == 1)
    {

    }
    else if (userinput == 2)
    {
        ListBoardingGates();
    }
    else if (userinput == 3)
    {

    }
    else if (userinput == 4)
    {

    }
    else if (userinput == 5)
    {
        ListAirlines();
    }
    else if (userinput == 6)
    {

    }
    else if (userinput == 7)
    {

    }
}
*/
