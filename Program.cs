using CSV_FileReader;

public class Program
{

    private static ApplicationDbContext _context = new ApplicationDbContext();
    private static ReaderService readerService = new ReaderService(_context);
    private static void Main(string[] args)
    {
        Console.Clear();

        Console.WriteLine("\n\n\t************************************");
        Console.WriteLine("\t*       Zulassungsstatistik        *");
        Console.WriteLine("\t************************************");

        Console.Write("\n\tCSV-Datei für Monate: ");
        string filePathMonths = Console.ReadLine();//filepath of month csv
        bool isFileLoaded = false;//for while-loop
        while (!isFileLoaded)//while the file is not loaded this loop is active
        {
            try
            {
                //C:\Users\kimu\Desktop\LAP\Zulassungen\Monate.csv
                Console.Write("\n\tLädt...");
                readerService.ImportMonths(filePathMonths);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tMonate erfolgreich geladen.");
                Console.ForegroundColor = ConsoleColor.White; 
                isFileLoaded = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n\tFehler beim Laden der Datei: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\tGültiger Pfad: ");
                filePathMonths = Console.ReadLine();
            }
        }

        Console.Write("\n\n\tCSV-Datei für Marken: ");
        string filePathManufacturer = Console.ReadLine();
        isFileLoaded = false;
        while (!isFileLoaded)
        {
            try
            {
                //C:\Users\kimu\Desktop\LAP\Zulassungen\Marken.csv
                Console.Write("\n\tLädt...");
                readerService.ImportManufacturer(filePathManufacturer);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tMarken erfolgreich geladen.");
                Console.ForegroundColor = ConsoleColor.White;
                isFileLoaded = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n\tFehler beim Laden der Datei: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\tGültiger Pfad: ");
                filePathManufacturer = Console.ReadLine();
            }
        }

        Console.Write("\n\n\tCSV-Datei für Zulassugen: ");
        string filePathRegistrations = Console.ReadLine();
        isFileLoaded = false;
        while (!isFileLoaded)
        {
            try
            {
                //C:\Users\kimu\Desktop\LAP\Zulassungen\Zulassungen.csv
                Console.Write("\n\tLädt...");
                readerService.ImportRegistrations(filePathRegistrations);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tZulassungen erfolgreich geladen.");
                Console.ForegroundColor = ConsoleColor.White;
                isFileLoaded = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n\tFehler beim Laden der Datei: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White; 
                Console.Write("\n\tGültiger Pfad: ");
                filePathRegistrations = Console.ReadLine();
            }
        }

        Console.Write("\n\tTaste drücken um fortzufahren...");
        Console.Read();

        bool showMenu = true;
        while (showMenu)//while 4(exit) is not being called the menu is getting shown
        {
            showMenu = MainMenu();
        }
    }

    private static bool MainMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\n\t************************************");
        Console.WriteLine("\t*     Zulassungsstatistik          *");
        Console.WriteLine("\t************************************");
        Console.WriteLine("\t*   Option auswählen:              *");
        Console.WriteLine("\t************************************");

        Console.WriteLine("\t* 1) Top 3 zugelassene Marken      *");
        Console.WriteLine("\t* 2) Monat mit meisten Zulassungen *");
        Console.WriteLine("\t* 3) Jahr mit meisten Renaults     *");
        Console.WriteLine("\t* 4) Beenden                       *");
        Console.WriteLine("\t************************************");

        Console.Write("\n\t  Option auswählen: ");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("\n\tTop 3 Marken mit den meisten Zulassungen\n");
                foreach(var manufacturer in readerService.MostRegisteredManufacturers())
                {
                    Console.WriteLine("\t   -" + manufacturer);
                }
                Console.Read();
                return true;
            case "2":
                Console.WriteLine("\n\tMonat mit meisten Zulassungen\n");
                Console.WriteLine("\t   -" + readerService.MonthWithMostRegistrations());
                Console.Read();
                return true;
            case "3":
                Console.WriteLine("\n\tJahr mit meisten Renaults Zulassungen\n");
                Console.WriteLine("\t   -" + readerService.YearWithMostRegisteredRenaults());
                Console.Read();
                return true;
            case "4":
                return false;

            default:
                return true;
        }
    }
}