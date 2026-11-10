namespace SmartStayHotel;

// the first menu the user sees. The program ends when the user chooses Exit
public class KU2703056_MainMenu
{
    private readonly KU2703056_AdminMenu adminMenu;
    private readonly KU2703056_GuestMenu guestMenu;

    public KU2703056_MainMenu(KU2703056_AdminMenu adminMenu, KU2703056_GuestMenu guestMenu)
    {
        this.adminMenu = adminMenu;
        this.guestMenu = guestMenu;
    }

    public void Run()
    {
        Console.WriteLine("****************************************");
        Console.WriteLine("*   WELCOME TO SMARTSTAY HOTEL BOOKING *");
        Console.WriteLine("****************************************");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("============== MAIN MENU ==============");
            Console.WriteLine("1. Administrator");
            Console.WriteLine("2. Guest");
            Console.WriteLine("0. Exit");

            int choice = KU2703056_ConsoleInput.ReadInt("Choose an option: ");

            switch (choice)
            {
                case 1: adminMenu.Run(); break;
                case 2: guestMenu.Run(); break;
                case 0: exit = true; break;
                default: Console.WriteLine("Invalid option. Please choose from the menu."); break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Thank you for using SmartStay Hotel Booking System. Goodbye!");
        KU2703056_ConsoleInput.Pause();
    }
}
