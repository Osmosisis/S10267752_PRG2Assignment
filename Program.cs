using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using S10267752_PRGassignment2;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================


Terminal term5 = new Terminal();

// Basic Feature 1
void initAirlines()
{
    string[] a = File.ReadAllLines("airlines.csv");
    foreach (string s in a)
    {
        string[] t = s.Split(',');
        Airline airinit = new Airline(t[0], t[1]);
        if (!term5.Airlines.ContainsKey(t[1]))
        {
            term5.Airlines.Add(t[1], airinit); 
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
        if (!term5.BoardingGates.ContainsKey(gateName))
        {
            term5.BoardingGates.Add(gateName, gate);
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

        term5.Flights.Add(t[0], f); //changed it to this as the keys need to be unique or we cant add all the flights

        string airlinecode = t[0].Substring(0,2);
        term5.Airlines[airlinecode].AddFlight(f);
    }
}


initAirlines();
initBoardingGates();
initFlights();


// Basic Feature 3

void ListAllFlights()
{
    foreach(KeyValuePair<string,Airline> a in term5.Airlines)
    {
        Console.WriteLine(a.Value.ToString());
    }

}


// Basic Feature 4
void ListBoardingGates()
{
    Console.Write(@"=============================================
List of Boarding Gates for Changi Airport Terminal 5
=============================================
");
    Console.WriteLine("Gate Name \tDDJB \tCFFT \tLWTT \tFlight Assigned");
    foreach (var gate in term5.BoardingGates.Values)
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
    Flight chosenflight = term5.Flights[flightno]; // made it a Flight obj
    string airlinecode = flightno.Substring(0,2);

    Airline airline = term5.Airlines[airlinecode];
    Console.WriteLine(airline.Flights[flightno].ToString());





    Console.WriteLine("Enter Boarding Gate Name:");
    String? boardinggate;
    boardinggate = Console.ReadLine();
    while (true)
    {
        if (term5.BoardingGates[boardinggate].Flight == null)
        {
            term5.BoardingGates[boardinggate].Flight = chosenflight; // added flight to boarding gate
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
            airline.Flights[flightno].Status = "Delayed";
        }
        else if (option == "2")
        {
            airline.Flights[airlinecode].Status = "Boarding";
        }
        else if (option == "3")
        {
            airline.Flights[airlinecode].Status = "On Time";
        }


    }
    Console.WriteLine($"Flight {flightno} has been assigned to Boarding Gate {boardinggate}!");
}


// Basic Feature 6
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
        term5.Airlines[airlinecode].AddFlight(f);
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
    foreach (var air in term5.Airlines.Keys)
    {
        Console.WriteLine($"{term5.Airlines[air].Code,-15} {term5.Airlines[air].Name}");
    }

    // Prompt user for Airline Code
    Console.Write("Enter Airline Code: ");
    string aircode = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

    // Validate Airline Code
    if (string.IsNullOrEmpty(aircode))
    {
        Console.WriteLine("Error: Airline Code cannot be empty. Please try again.");
        return;
    }

    if (!term5.Airlines.ContainsKey(aircode))
    {
        Console.WriteLine($"Error: Airline Code '{aircode}' is invalid. Please enter a valid Airline Code.");
        return;
    }

    // Display flights for the selected airline
    Console.WriteLine($"=============================================\r\nList of Flights for {term5.Airlines[aircode].Name}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-18} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time"}");

    bool hasFlights = false; // Flag to check if any flights exist for the airline
    foreach (var f in term5.Flights.Keys)
    {
        if (f.Substring(0, 2) == aircode)
        {
            Console.Write($"\n{term5.Flights[f].FlightNumber,-15} {term5.Airlines[aircode].Name,-18} {term5.Flights[f].Origin,-15} {term5.Flights[f].Destination,-15} {term5.Flights[f].ExpectedTime}");
            hasFlights = true;
        }
    }

    // If no flights are found for the airline
    if (!hasFlights)
    {
        Console.WriteLine($"\nNo flights found for {term5.Airlines[aircode].Name}.");
    }
}
// Basic Feature 8
void ModifyFlightDetails()
{
    Console.WriteLine(@"
=============================================
List of Airlines for Changi Airport Terminal 5
=============================================
");
    foreach (var air in term5.Airlines.Keys)
    {
        Console.WriteLine($"{term5.Airlines[air].Code,-15} {term5.Airlines[air].Name}");
    }

    // Prompt for Airline Code
    Console.Write("Enter Airline Code: ");
    string aircode = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

    // Validate Airline Code
    if (string.IsNullOrEmpty(aircode) || !term5.Airlines.ContainsKey(aircode))
    {
        Console.WriteLine("Error: Invalid Airline Code. Please enter a valid 2-letter Airline Code.");
        return;
    }

    Console.WriteLine($"=============================================\r\nList of Flights for {term5.Airlines[aircode].Name}\r\n=============================================\r\n");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-18} {"Origin",-15} {"Destination",-15} {"Expected Departure/Arrival Time"}");

    bool hasFlights = false; // Flag to check if any flights exist for the airline
    foreach (var f in term5.Flights.Keys)
    {
        if (f.Substring(0, 2) == aircode)
        {
            Console.Write($"\n{term5.Flights[f].FlightNumber,-15} {term5.Airlines[aircode].Name,-18} {term5.Flights[f].Origin,-15} {term5.Flights[f].Destination,-15} {term5.Flights[f].ExpectedTime}");
            hasFlights = true;
        }
    }

    // If no flights are found for the airline
    if (!hasFlights)
    {
        Console.WriteLine($"\nNo flights found for {term5.Airlines[aircode].Name}.");
        return;
    }

    // Prompt for Flight Number to modify/delete
    Console.Write("\nChoose an existing Flight to modify or delete: ");
    string flight2change = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

    // Validate Flight Number
    if (string.IsNullOrEmpty(flight2change) || !term5.Flights.ContainsKey(flight2change))
    {
        Console.WriteLine("Error: Invalid Flight Number. Please enter a valid Flight Number.");
        return;
    }

    Flight chosenflight = term5.Flights[flight2change];

    // Prompt for action (Modify or Delete)
    Console.Write(@"
1. Modify Flight
2. Delete Flight
Choose an option: ");
    if (!int.TryParse(Console.ReadLine(), out int chosen) || chosen < 1 || chosen > 2)
    {
        Console.WriteLine("Error: Invalid option. Please enter 1 (Modify) or 2 (Delete).");
        return;
    }

    if (chosen == 1) // Modify Flight
    {
        Console.Write(@"
1. Modify Basic Information
2. Modify Status
3. Modify Special Request Code
4. Modify Boarding Gate
Choose an option: ");
        if (!int.TryParse(Console.ReadLine(), out int secondchosen) || secondchosen < 1 || secondchosen > 4)
        {
            Console.WriteLine("Error: Invalid option. Please enter a number between 1 and 4.");
            return;
        }

        if (secondchosen == 1) // Modify Basic Information
        {
            Console.Write("Enter new Origin: ");
            string newOrigin = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newOrigin))
            {
                Console.WriteLine("Error: Origin cannot be empty. Please enter a valid Origin.");
                return;
            }
            term5.Flights[flight2change].Origin = newOrigin;

            Console.Write("Enter new Destination: ");
            string newDestination = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newDestination))
            {
                Console.WriteLine("Error: Destination cannot be empty. Please enter a valid Destination.");
                return;
            }
            term5.Flights[flight2change].Destination = newDestination;

            Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newTime))
            {
                Console.WriteLine("Error: Invalid date/time format. Please use the format dd/MM/yyyy HH:mm.");
                return;
            }
            term5.Flights[flight2change].ExpectedTime = newTime;
        }
        else if (secondchosen == 2) // Modify Status
        {
            Console.Write("Enter new Status [On Time/Delayed/Boarding]: ");
            string newStatus = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newStatus) || (newStatus != "On Time" && newStatus != "Delayed" && newStatus != "Boarding"))
            {
                Console.WriteLine("Error: Invalid Status. Please enter 'On Time', 'Delayed', or 'Boarding'.");
                return;
            }
            term5.Flights[flight2change].Status = newStatus;
        }
        else if (secondchosen == 3) // Modify Special Request Code
        {
            Console.Write("Enter new Special Request Code [DDJB/CFFT/LWTT/NORM]: ");
            string newRequestCode = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(newRequestCode) || (newRequestCode != "DDJB" && newRequestCode != "CFFT" && newRequestCode != "LWTT" && newRequestCode != "NORM"))
            {
                Console.WriteLine("Error: Invalid Special Request Code. Please enter 'DDJB', 'CFFT', 'LWTT', or 'NORM'.");
                return;
            }

            // Update the flight type based on the new request code
            if (newRequestCode == "DDJB" && chosenflight is not DDJBFlight)
            {
                chosenflight = new DDJBFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to DDJBFlight.");
            }
            else if (newRequestCode == "CFFT" && chosenflight is not CFFTFlight)
            {
                chosenflight = new CFFTFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to CFFTFlight.");
            }
            else if (newRequestCode == "LWTT" && chosenflight is not LWTTFlight)
            {
                chosenflight = new LWTTFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to LWTTFlight.");
            }
            else if (newRequestCode == "NORM" && chosenflight is not NORMFlight)
            {
                chosenflight = new NORMFlight(chosenflight.FlightNumber, chosenflight.Origin, chosenflight.Destination, chosenflight.ExpectedTime);
                Console.WriteLine("Flight updated to NORMFlight.");
            }
            term5.Flights[flight2change] = chosenflight; // Update the flight in the dictionary
        }
        else if (secondchosen == 4) // Modify Boarding Gate
        {
            Console.Write("Enter new Boarding Gate: ");
            string boardGateName = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

            // Validate Boarding Gate
            if (string.IsNullOrEmpty(boardGateName) || !term5.BoardingGates.ContainsKey(boardGateName))
            {
                Console.WriteLine("Error: Invalid Boarding Gate. Please enter a valid Boarding Gate.");
                return;
            }

            BoardingGate chosenBoard = term5.BoardingGates[boardGateName];

            // Check if the boarding gate is already assigned to another flight
            if (chosenBoard.Flight != null && chosenBoard.Flight != chosenflight)
            {
                Console.WriteLine("Error: The selected Boarding Gate is already assigned to another flight.");
                return;
            }

            // Assign the flight to the new boarding gate
            chosenBoard.Flight = chosenflight;

            // Remove the flight from any other boarding gates
            foreach (var gate in term5.BoardingGates.Values)
            {
                if (gate.GateName != boardGateName && gate.Flight == chosenflight)
                {
                    gate.Flight = null;
                }
            }
        }

        // Display updated flight details
        Console.WriteLine($"Flight Updated!\nFlight Number: {term5.Flights[flight2change].FlightNumber}\nAirline Name: {term5.Airlines[aircode].Name}\nOrigin: {term5.Flights[flight2change].Origin}\nDestination: {term5.Flights[flight2change].Destination}\nExpected Departure/Arrival Time: {term5.Flights[flight2change].ExpectedTime}");
        if (term5.Flights[flight2change].Status != null)
        {
            Console.WriteLine($"\nStatus: {term5.Flights[flight2change].Status}");
        }
        foreach (var gate in term5.BoardingGates.Values)
        {
            if (gate.Flight == chosenflight)
            {
                Console.WriteLine($"\nBoarding Gate: {gate.GateName}");
            }
        }
    }
    else if (chosen == 2) // Delete Flight
    {
        Console.Write("Are you sure you want to delete the flight? (Y/N): ");
        string option = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

        if (option == "Y")
        {
            term5.Flights.Remove(flight2change);
            Console.WriteLine($"Flight {flight2change} has been deleted.");
        }
        else
        {
            Console.WriteLine("Flight deletion canceled.");
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
        ListAllFlights();
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
        ModifyFlightDetails();
    }
    else if (userinput == 7)
    {

    }
}

