using KU2703056_SmartStay;
using System.Globalization;

namespace SmartStayHotel;

// reads and checks user input from the console
// every method keeps asking until the input is valid
public static class KU2703056_ConsoleInput
{
    // prints a title between two lines
    public static void ShowTitle(string title)
    {
        Console.WriteLine();
        Console.WriteLine("----- " + title + " -----");
    }

    // waits for the user to press Enter
    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    public static string ReadText(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? "").Trim();
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            string text = ReadText(prompt);
            if (int.TryParse(text, out int value))
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please enter a whole number.");
        }
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            string text = ReadText(prompt + " (dd/MM/yyyy): ");
            if (TryParseDate(text, out DateTime date))
            {
                return date;
            }
            Console.WriteLine("Invalid date. Use the format dd/MM/yyyy, for example 25/12/2026.");
        }
    }

    // same as ReadDate, but pressing Enter without typing returns null
    public static DateTime? ReadOptionalDate(string prompt)
    {
        while (true)
        {
            string text = ReadText(prompt + " (dd/MM/yyyy, or press Enter for all): ");
            if (text == "")
            {
                return null;
            }
            if (TryParseDate(text, out DateTime date))
            {
                return date;
            }
            Console.WriteLine("Invalid date. Use the format dd/MM/yyyy, for example 25/12/2026.");
        }
    }

    public static KU2703056_RoomType ReadRoomType()
    {
        Console.WriteLine("Room types: 1) Standard  2) Deluxe  3) Suite  4) Family Suite");
        while (true)
        {
            int choice = ReadInt("Choose room type (1-4): ");
            if (choice >= 1 && choice <= 4)
            {
                return (KU2703056_RoomType)(choice - 1);
            }
            Console.WriteLine("Please choose a number from 1 to 4.");
        }
    }

    public static KU2703056_BedType ReadBedType()
    {
        Console.WriteLine("Bed types: 1) Single  2) Double  3) Queen  4) King");
        while (true)
        {
            int choice = ReadInt("Choose bed type (1-4): ");
            if (choice >= 1 && choice <= 4)
            {
                return (KU2703056_BedType)(choice - 1);
            }
            Console.WriteLine("Please choose a number from 1 to 4.");
        }
    }

    public static KU2703056_AvailabilityStatus ReadStatus()
    {
        Console.WriteLine("Status: 1) Available  2) Reserved  3) Under Maintenance");
        while (true)
        {
            int choice = ReadInt("Choose status (1-3): ");
            if (choice >= 1 && choice <= 3)
            {
                return (KU2703056_AvailabilityStatus)(choice - 1);
            }
            Console.WriteLine("Please choose a number from 1 to 3.");
        }
    }

    private static bool TryParseDate(string text, out DateTime date)
    {
        return DateTime.TryParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                      DateTimeStyles.None, out date);
    }
}
