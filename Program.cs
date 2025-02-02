using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization;
using S10267752_PRGassignment2;
//==========================================================
// Student Number	: S10267752
// Student Name	: Osmond Lim
// Partner Name	: Yoshihiro Chan
//==========================================================

Terminal term5 = new Terminal();

// Basic Feature 1
int initAirlines()
{
    int total = 0;
    string[] a = File.ReadAllLines("airlines.csv");
    foreach (string s in a.Skip(1))
    {
        string[] t = s.Split(',');
        Airline airinit = new Airline(t[0], t[1]);
        if (!term5.Airlines.ContainsKey(t[1]))
        {
            term5.Airlines.Add(t[1], airinit); 
            total++;
        }
    }

    return total;
}
int initBoardingGates()
{
    int total = 0;
    string[] a = File.ReadAllLines("boardinggates.csv");
    foreach (string line in a.Skip(1)) 
    {
        string[] parts = line.Split(',');
        // Parse and validate the data
        string gateName = parts[0];
        bool supportsDDJB = bool.TryParse(parts[1], out bool ddjb) && ddjb;
        bool supportsCFFT = bool.TryParse(parts[2], out bool cfft) && cfft;
        bool supportsLWTT = bool.TryParse(parts[3], out bool lwtt) && lwtt;

        // Create the BoardingGate object
        BoardingGate gate = new BoardingGate(gateName, supportsDDJB, supportsCFFT, supportsLWTT);

        // Add to the newboard dictionary
        if (!term5.BoardingGates.ContainsKey(gateName))
        {
            term5.BoardingGates.Add(gateName, gate);
            total++;
        }
    }
    return total;
}
// Basic Feature 2
int initFlights()
{   
    int total = 0;
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
        total++;
    }
    return total;
}




// Basic Feature 3

void ListAllFlights()
{
    // List<Flight> flightlist = new List<Flight>();
    // foreach(KeyValuePair<string,Airline> a in term5.Airlines)
    // {
    //     foreach(KeyValuePair<string,Flight> f in a.Value.Flights)
    //     {
    //         flightlist.Add(f.Value);
    //     }
        
    // }     
    // flightlist.Sort();
    // foreach(Flight f in flightlist)
    // {
    //         Console.WriteLine(f.ToString());
    // }

    foreach(KeyValuePair<string,Airline> a in term5.Airlines)
    {
        System.Console.WriteLine(a.Value.ToString());
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
    string airlinecode = flightno.Substring(0, 2);

    Airline airline = term5.Airlines[airlinecode];
    Console.WriteLine(airline.Flights[flightno].ToString());





    Console.WriteLine("Enter Boarding Gate Name:");
    String? boardinggate;


    while (true)
    {
        bool cont = true;
        boardinggate = Console.ReadLine();
        if (chosenflight is LWTTFlight)
        {
            if (term5.BoardingGates[boardinggate].SupportLWTT == false)
            {
                System.Console.WriteLine("Gate does not support LWTT. Please Re-Enter Boarding Gate Name: ");
                cont = false;
            }
        }
        else if (chosenflight is DDJBFlight)
        {
            if (term5.BoardingGates[boardinggate].SupportDDJB == false)
            {
                System.Console.WriteLine("Gate does not support DDJB. Please Re-Enter Boarding Gate Name: ");
                cont = false;
            }
        }
        else if (chosenflight is CFFTFlight)
        {
            if (term5.BoardingGates[boardinggate].SupportCFFT == false)
            {
                System.Console.WriteLine("Gate does not support CFFT. Please Re-Enter Boarding Gate Name: ");
                cont = false;
            }
        }
        if (cont == true)
        {
            if (term5.BoardingGates[boardinggate].Flight == null)
            {
                term5.BoardingGates[boardinggate].Flight = chosenflight; // added flight to boarding gate
                break;
            }
            else
            {
                Console.Write("Boarding gate is already assigned to another flight. Please Re-Enter Boarding Gate Name: ");
            }
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
            airline.Flights[flightno].Status = "Boarding";
        }
        else if (option == "3")
        {
            airline.Flights[flightno].Status = "On Time";
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
        try
        {
            // Validate Flight Number
            string flightno = "";
            while (string.IsNullOrEmpty(flightno) || flightno.Length < 2)
            {
                Console.WriteLine("Enter Flight Number: ");
                flightno = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(flightno) || flightno.Length < 2)
                {
                    Console.WriteLine("Error: Flight number cannot be empty and must be at least 2 characters long.");
                }
            }

            // Validate Destination
            string destination = "";
            while (string.IsNullOrEmpty(destination))
            {
                Console.WriteLine("Enter Destination: ");
                destination = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(destination))
                {
                    Console.WriteLine("Error: Destination cannot be empty.");
                }
            }

            // Validate Origin
            string origin = "";
            while (string.IsNullOrEmpty(origin))
            {
                Console.WriteLine("Enter Origin: ");
                origin = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(origin))
                {
                    Console.WriteLine("Error: Origin cannot be empty.");
                }
            }

            // Validate Expected Departure/Arrival Time
            DateTime expectedtime = DateTime.MinValue;
            bool isValidTime = false;
            while (!isValidTime)
            {
                Console.WriteLine("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm):");
                string timeInput = Console.ReadLine()?.Trim();
                if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedtime))
                {
                    isValidTime = true;
                }
                else
                {
                    Console.WriteLine("Error: Invalid date/time format. Please use the format dd/MM/yyyy HH:mm.");
                }
            }

            // Validate Special Request Code
            string src = "";
            bool isValidSrc = false;
            while (!isValidSrc)
            {
                Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None):");
                src = Console.ReadLine()?.Trim().ToUpper();
                if (src == "CFFT" || src == "DDJB" || src == "LWTT" || src == "NONE")
                {
                    isValidSrc = true;
                }
                else
                {
                    Console.WriteLine("Error: Invalid Special Request Code. Please enter 'CFFT', 'DDJB', 'LWTT', or 'None'.");
                }
            }

            // Create Flight Object
            Flight f = null;
            if (src == "DDJB")
            {
                f = new DDJBFlight(flightno, destination, origin, expectedtime);
            }
            else if (src == "LWTT")
            {
                f = new LWTTFlight(flightno, destination, origin, expectedtime);
            }
            else if (src == "CFFT")
            {
                f = new CFFTFlight(flightno, destination, origin, expectedtime);
            }
            else
            {
                f = new NORMFlight(flightno, destination, origin, expectedtime);
                src = "";
            }

            // Add Flight to Airline
            string airlinecode = flightno.Substring(0, 2);
            if (term5.Airlines.ContainsKey(airlinecode))
            {
                term5.Airlines[airlinecode].AddFlight(f);
            }
            else
            {
                Console.WriteLine($"Error: Airline code '{airlinecode}' not found. Flight not added.");
                continue;
            }

            // Append Flight to CSV File
            try
            {
                File.AppendAllText("flights.csv", $"{f.FlightNumber},{f.Origin},{f.Destination},{f.ExpectedTime},{src}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to write to file. Details: {ex.Message}");
            }

            // Display CSV File Contents
            try
            {
                string[] a = File.ReadAllLines("flights.csv");
                string[] header = a[0].Split(",");
                foreach (string s in a.Skip(1))
                {
                    Console.WriteLine(s);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Unable to read file. Details: {ex.Message}");
            }

            // Confirm Flight Addition
            Console.WriteLine($"Flight {flightno} has been added!");

            // Prompt to Add Another Flight
            Console.WriteLine("Would you like to add another flight? (Y/N)");
            string option = Console.ReadLine()?.Trim().ToUpper();
            if (option == "N")
            {
                cont = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: An unexpected error occurred. Details: {ex.Message}");
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
    Console.WriteLine($"{"Airline Code", -15} {"Airline Name"}");
    foreach (var air in term5.Airlines.Keys)
    {
        Console.WriteLine($"{term5.Airlines[air].Code,-15} {term5.Airlines[air].Name}");
    }
    string? aircode;
    while (true)
    {
        // Prompt for Airline Code
        Console.Write("Enter Airline Code: ");
        aircode = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

        // Validate Airline Code
        if (string.IsNullOrEmpty(aircode) || !term5.Airlines.ContainsKey(aircode))
        {
            Console.WriteLine("Error: Invalid Airline Code. Please enter a valid 2-letter Airline Code.");
        }
        else
            break;
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

    string? flight2change;
    while (true)
    {
        // Prompt for Flight Number to modify/delete
        Console.Write("\nChoose an existing Flight to modify or delete: ");
        flight2change = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

        // Validate Flight Number
        if (string.IsNullOrEmpty(flight2change) || !term5.Flights.ContainsKey(flight2change))
        {
            Console.WriteLine("Error: Invalid Flight Number. Please enter a valid Flight Number.");
        }
        else
            break;
    }
    Flight chosenflight = term5.Flights[flight2change];
    // Prompt for action (Modify or Delete)
    int chosen = 0;
    bool isValidInput = false;

    while (!isValidInput)
    {
        Console.Write(@"
1. Modify Flight
2. Delete Flight
Choose an option: ");
        string input = Console.ReadLine();

        // Validate input
        if (int.TryParse(input, out chosen) && (chosen == 1 || chosen == 2))
        {
            isValidInput = true; // Exit the loop if input is valid
        }
        else
        {
            Console.WriteLine("Error: Invalid option. Please enter 1 (Modify) or 2 (Delete).");
        }
    }


    if (chosen == 1) // Modify Flight
    {
        int secondchosen = 0;
        bool isValidSecondInput = false;

        while (!isValidSecondInput)
        {
            Console.Write(@"
1. Modify Basic Information
2. Modify Status
3. Modify Special Request Code
4. Modify Boarding Gate
Choose an option: ");
            string input = Console.ReadLine();

            // Validate input
            if (int.TryParse(input, out secondchosen) && secondchosen >= 1 && secondchosen <= 4)
            {
                isValidSecondInput = true; // Exit the loop if input is valid
            }
            else
            {
                Console.WriteLine("Error: Invalid option. Please enter a number between 1 and 4.");
            }
        }

        if (secondchosen == 1) // Modify Basic Information
        {
            // Prompt for new Origin
            string newOrigin = "";
            while (string.IsNullOrEmpty(newOrigin))
            {
                Console.Write("Enter new Origin: ");
                newOrigin = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(newOrigin))
                {
                    Console.WriteLine("Error: Origin cannot be empty. Please enter a valid Origin.");
                }
            }
            term5.Flights[flight2change].Origin = newOrigin;

            // Prompt for new Destination
            string newDestination = "";
            while (string.IsNullOrEmpty(newDestination))
            {
                Console.Write("Enter new Destination: ");
                newDestination = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(newDestination))
                {
                    Console.WriteLine("Error: Destination cannot be empty. Please enter a valid Destination.");
                }
            }
            term5.Flights[flight2change].Destination = newDestination;

            // Prompt for new Expected Departure/Arrival Time
            DateTime newTime = default; // Declare newTime outside the loop
            bool isValidTime = false;
            while (!isValidTime)
            {
                Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                string timeInput = Console.ReadLine();

                if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out newTime))
                {
                    isValidTime = true; // Exit the loop if the input is valid
                }
                else
                {
                    Console.WriteLine("Error: Invalid date/time format. Please use the format dd/MM/yyyy HH:mm.");
                }
            }
            term5.Flights[flight2change].ExpectedTime = newTime;
        }
        else if (secondchosen == 2) // Modify Status
        {
            string newStatus = "";
            bool isValidStatus = false;

            while (!isValidStatus)
            {
                Console.Write("Enter new Status [On Time/Delayed/Boarding]: ");
                newStatus = Console.ReadLine()?.Trim();

                // Validate the status
                if (!string.IsNullOrEmpty(newStatus) && (newStatus == "On Time" || newStatus == "Delayed" || newStatus == "Boarding"))
                {
                    isValidStatus = true; // Exit the loop if the input is valid
                }
                else
                {
                    Console.WriteLine("Error: Invalid Status. Please enter 'On Time', 'Delayed', or 'Boarding'.");
                }
            }
            term5.Flights[flight2change].Status = newStatus;
        }
        else if (secondchosen == 3) // Modify Special Request Code
        {
            bool isValidGate = false;
            string newRequestCode = "";
            while (!isValidGate)
            {
                Console.Write("Enter new Special Request Code [DDJB/CFFT/LWTT/NORM]: ");
                newRequestCode = Console.ReadLine()?.Trim().ToUpper();
                if (!string.IsNullOrEmpty(newRequestCode) && (newRequestCode == "DDJB" || newRequestCode == "CFFT" || newRequestCode == "LWTT" || newRequestCode == "NORM"))
                {
                    isValidGate = true;
                }
                else
                    Console.WriteLine("Error: Invalid Special Request Code. Please enter 'DDJB', 'CFFT', 'LWTT', or 'NORM'.");
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
            string boardGateName = "";
            bool isValidGate = false;
            BoardingGate chosenBoard;

            while (!isValidGate)
            {
                Console.Write("Enter new Boarding Gate: ");
                boardGateName = Console.ReadLine()?.Trim().ToUpper(); // Normalize input

                // Validate Boarding Gate
                if (string.IsNullOrEmpty(boardGateName) || !term5.BoardingGates.ContainsKey(boardGateName))
                {
                    Console.WriteLine("Error: Invalid Boarding Gate. Please enter a valid Boarding Gate.");
                    continue; // Prompt again for boarding gate
                }

                chosenBoard = term5.BoardingGates[boardGateName];

                // Check if the boarding gate is already assigned to another flight
                if (chosenBoard.Flight != null && chosenBoard.Flight != chosenflight)
                {
                    Console.WriteLine("Error: The selected Boarding Gate is already assigned to another flight.");
                    continue; // Prompt again for boarding gate
                }

                // If the boarding gate is valid and not assigned to another flight, exit the loop
                isValidGate = true;
            }

            // Assign the flight to the new boarding gate
            chosenBoard = term5.BoardingGates[boardGateName];
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


void DisplayFlightSchedule()
{
    List<Flight> flightlist = new List<Flight>();
    foreach (KeyValuePair<string, Airline> a in term5.Airlines)
    {
        foreach (KeyValuePair<string, Flight> f in a.Value.Flights)
        {
            flightlist.Add(f.Value);
        }

    }
    flightlist.Sort();
    foreach (Flight f in flightlist)
    {
        string src = null;
        string gate = "Unassigned";
        string airlinecode = f.FlightNumber.Substring(0, 2);
        if (f is DDJBFlight)
        {
            src = "DDJB";
        }
        else if (f is LWTTFlight)
        {
            src = "LWTT";
        }
        else if (f is CFFTFlight)
        {
            src = "CFFT";
        }
        else
        {
            src = "None";
        }

        foreach (KeyValuePair<string, BoardingGate> g in term5.BoardingGates)
        {
            if (g.Value.Flight != null)
            {
                if (g.Value.Flight.FlightNumber == f.FlightNumber)
                {
                    gate = g.Value.GateName;
                }
            }

        }
        //, Status, Special Request Code (if any) and Boarding Gate 
        Console.WriteLine($"Flight Number: {f.FlightNumber} \nAirline Name: {term5.Airlines[airlinecode].Name} \nOrigin: {f.Origin} \nDestination: {f.Destination} \nExpected Time: {f.ExpectedTime} \nStatus: {f.Status} \nSpecial Request Code: {src} \nBoarding Gate: {gate} \n");

    }
}
// Advanced Featrue (a)

// Advanced Feature (b)




// Main Running Code

int temp;
System.Console.WriteLine("Loading Airlines...");
temp = initAirlines();
System.Console.WriteLine($"{temp} Airlines Loaded!");
System.Console.WriteLine("Loading Boarding Gates...");
temp = initBoardingGates();
System.Console.WriteLine($"{temp} Boarding Gates Loaded!");
System.Console.WriteLine("Loading Flights...");
temp = initFlights();
System.Console.WriteLine($"{temp} Flights Loaded!");



while (true)
{
    try
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

        string input = Console.ReadLine();

        // Validate input
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Error: Input cannot be empty. Please enter a valid option.");
            continue; // Prompt again
        }

        if (!int.TryParse(input, out int userinput))
        {
            Console.WriteLine("Error: Invalid input. Please enter a number.");
            continue; // Prompt again
        }

        // Validate menu option
        if (userinput < 0 || userinput > 7)
        {
            Console.WriteLine("Error: Invalid option. Please enter a number between 0 and 7.");
            continue; // Prompt again
        }

        // Handle user input
        if (userinput == 0)
        {
            break;
        }
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
            CreateNewFlight();
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
            DisplayFlightSchedule();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: An unexpected error occurred. Details: {ex.Message}");
    }
}

