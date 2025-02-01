using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using S10267752_PRGassignment2;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================


Terminal Term5 = new Terminal();

// Basic Feature 1
void initAirlines()
{
    string[] a = File.ReadAllLines("airlines.csv");
    foreach (string s in a)
    {
        string[] t = s.Split(',');
        Airline airinit = new Airline(t[0], t[1]);
        if (!Term5.airlines.ContainsKey(t[1]))
        {
            Term5.airlines.Add(t[1], airinit); 
        }
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
        if (!Term5.boardingGates.ContainsKey(gateName))
        {
            Term5.boardingGates.Add(gateName, gate);
        }
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

        Term5.flights.Add(t[0], f); //changed it to this as the keys need to be unique or we cant add all the flights

        string airlinecode = t[0].Substring(0,2);
        Term5.airlines[airlinecode].AddFlight(f);
    }
}


initAirlines();
initBoardingGates();
initFlights();


// Basic Feature 3


// foreach(KeyValuePair<string,Airline> a in newair)
// {
//     System.Console.WriteLine(a.Value.ToString());
// }


// Basic Feature 4
void ListBoardingGates()
{
    Console.Write(@"=============================================
List of Boarding Gates for Changi Airport Terminal 5
=============================================
");
    Console.WriteLine("Gate Name \tDDJB \tCFFT \tLWTT \tFlight Assigned");
    foreach (var gate in Term5.boardingGates.Values)
    {
        if (gate.Flight != null)
        {
            Console.WriteLine($"{gate.GateName,-10} \t{gate.SupportDDJB} \t{gate.SupportCFFT} \t{gate.SupportLWTT} \t{gate.Flight.FlightNumber}");

        }
        else
        {
            Console.WriteLine($"{gate.GateName,-10} \t{gate.SupportDDJB} \t{gate.SupportCFFT} \t{gate.SupportLWTT} \t{"None"}");
        }
    }
}

// Basic Feature 5
void AssignBoardingGate()
{
    Console.WriteLine("Enter Flight Number: ");
    string flightno = Console.ReadLine();
    Flight chosenflight = Term5.flights[flightno]; // made it a Flight obj
    string airlinecode = flightno.Substring(0,2);

    Airline airline = Term5.airlines[airlinecode];
    Console.WriteLine(airline.flights[flightno].ToString());




    Console.WriteLine("Enter Boarding Gate Name:");
    String? boardinggate;
    boardinggate = Console.ReadLine();
    while (true)
    {
        if (Term5.boardingGates[boardinggate].Flight == null)
        {
            Term5.boardingGates[boardinggate].Flight = chosenflight; // added flight to boarding gate
            break;
        }
        else
        {
            Console.Write("Boarding gate is already assigned to another flight. Please Re-Enter Boarding Gate Name: ");
            boardinggate = Console.ReadLine();
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


// Basic Feature 6
/*




N
*/

void CreateNewFlight()
{
    bool cont = true;
    while (cont == true)
    {

    
        Console.WriteLine("Enter Flight Number: ");
        string flightno = Console.ReadLine();
        Console.WriteLine("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.WriteLine("Enter Origin:");
        string origin = Console.ReadLine();
        Console.WriteLine("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm):");
        DateTime expectedtime = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None):");
        string src = Console.ReadLine();
        Flight f = null;

        if (src == "DDJB")
        {
            f = new DDJBFlight(flightno,destination,origin,expectedtime);
        }
        else if (src == "LWTT")
        {
            f = new LWTTFlight(flightno,destination,origin,expectedtime);
        }
        else if (src == "CFFT")
        {
            f = new CFFTFlight(flightno,destination,origin,expectedtime);
        }
        else
        {
            f = new NORMFlight(flightno,destination,origin,expectedtime);
        }

        
        string airlinecode = flightno.Substring(0,2);
        Term5.airlines[airlinecode].AddFlight(f);
        System.Console.WriteLine($"Flight {flightno} has been added!");
        System.Console.WriteLine("Would you like to add another flight? (Y/N)");
        string option = Console.ReadLine();
        if (option == "N")
        {
            cont = false;
        }

    }
}



// Basic Feature 7
void ListAirlines()
{
    Console.WriteLine(@"
=============================================
List of Airlines for Changi Airport Terminal 5
=============================================
");
    foreach (var air in Term5.airlines.Keys)
    {
        Console.WriteLine($"{Term5.airlines[air].Code, -15} {Term5.airlines[air].Name}");
    }
    Console.Write("Enter Airline Code: ");
    string aircode = Console.ReadLine();
    Console.WriteLine($"=============================================\r\nList of Flights for {Term5.airlines[aircode].Name}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number", -15} {"Airline Name", -18} {"Origin", -15} {"Destination", -15} {"Expected Departure/Arrival Time"}");
    foreach (var f in Term5.flights.Keys)
    {
        if (f.Substring(0, 2) == aircode)
        {
            Console.Write($"\n{Term5.flights[f].FlightNumber, -15} {Term5.airlines[aircode].Name,-18} {Term5.flights[f].Origin,-15} {Term5.flights[f].Destination,-15} {Term5.flights[f].ExpectedTime}");
        }
    }
}
// Basic Feature 8
void modifyFlightDetails()
{
    Console.WriteLine(@"
=============================================
List of Airlines for Changi Airport Terminal 5
=============================================
");
    foreach (var air in Term5.airlines.Keys)
    {
        Console.WriteLine($"{Term5.airlines[air].Code,-15} {Term5.airlines[air].Name}");
    }
    Console.Write("Enter Airline Code: ");
    string aircode = Console.ReadLine();
    Console.WriteLine($"=============================================\r\nList of Flights for {Term5.airlines[aircode].Name}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-18} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time"}");
    foreach (var f in Term5.flights.Keys)
    {
        if (f.Substring(0, 2) == aircode)
        {
            Console.Write($"\n{Term5.flights[f].FlightNumber,-15} {Term5.airlines[aircode].Name,-18} {Term5.flights[f].Origin,-15} {Term5.flights[f].Destination,-15} {Term5.flights[f].ExpectedTime}");
        }
    }
    Console.Write("\nChoose an existing Flight to modify or delete: ");
    string flight2change = Console.ReadLine();
    Flight chosenflight = Term5.flights[flight2change];
    Console.Write(@"
1. Modify Flight
2. Delete Flight
Choose an option: ");
    int chosen = Convert.ToInt32( Console.ReadLine());
    if (chosen == 1)
    {
        Console.Write(@"
1. Modify Basic Information
2. Modify Status
3. Modify Special Request Code
4. Modify Boarding Gate
Choose an option: ");
        int secondchosen = Convert.ToInt32(Console.ReadLine());
        if (secondchosen == 1)
        {
            Console.Write("Enter new Origin: ");
            Term5.flights[flight2change].Origin = Console.ReadLine();
            Console.Write("Enter new Destination: ");
            Term5.flights[flight2change].Destination = Console.ReadLine();
            Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
            Term5.flights[flight2change].ExpectedTime = Convert.ToDateTime(Console.ReadLine());
        }
        else if (secondchosen == 2)
        {
            Console.Write("Enter new Status [On Time/Delayed/Boarding]: ");
            Term5.flights[flight2change].Status = Console.ReadLine();
        }
        else if (secondchosen == 3)
        {
            Console.Write("Enter new Special Request Code: ");
            string newrequestcode = Console.ReadLine();
            if (newrequestcode == "DDJB" && chosenflight is not DDJBFlight) 
            {
                chosenflight = new DDJBFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to DDJBFlight.");
            }
            else if (newrequestcode == "CFFT" && chosenflight is not CFFTFlight)
            {
                chosenflight = new CFFTFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to CFFTFlight.");
            }
            else if (newrequestcode == "CFFT" && chosenflight is not LWTTFlight)
            {
                chosenflight = new LWTTFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to LWTTFlight.");
            }
            else if (newrequestcode == "NORM" && chosenflight is not NORMFlight)
            {
                chosenflight = new NORMFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to DDJBFlight.");
            }
        }
        else if (secondchosen == 4)
        {
            Console.Write("Enter new Boarding Gate: ");
            string boardgatename = Console.ReadLine();
            if (Term5.boardingGates.ContainsKey(boardgatename))
            {
                BoardingGate chosenboard = Term5.boardingGates[boardgatename];
                chosenboard.Flight = chosenflight;
            }
            foreach (var gate in Term5.boardingGates.Values)
            {
                if (gate.GateName != boardgatename && gate.Flight == chosenflight)
                {
                    gate.Flight = null;
                }
            }
        }
        Console.WriteLine($"Flight Updated!\nFlight Number: {Term5.flights[flight2change].FlightNumber}\nAirline Name: {Term5.airlines[aircode].Name}\nOrigin: {Term5.flights[flight2change].Origin}\nDestination: {Term5.flights[flight2change].Destination}\nExpected Departure/Arrival Time: {Term5.flights[flight2change].ExpectedTime}");
        if (Term5.flights[flight2change].Status != null)
        {
            Console.WriteLine($"\nStatus: {Term5.flights[flight2change].Status}"); // if flight obj has status, display
        }
        foreach (var gate in Term5.boardingGates.Values)
        {
            if (gate.Flight == chosenflight)
            {
                Console.WriteLine($"\nBoarding Gate: {gate.GateName}");
            }
        }
    }
    else if (chosen == 2)
    {
        Console.Write("Would you like to delete the flight? (Y/N)");
        string option = Console.ReadLine().ToLower();
        if (option == "y")
        {
            Term5.flights.Remove(flight2change);
        }
    }
}
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
    Console.Write(@"

=============================================
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
        AssignBoardingGate();
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
        modifyFlightDetails();
    }
    else if (userinput == 7)
    {

    }
}

