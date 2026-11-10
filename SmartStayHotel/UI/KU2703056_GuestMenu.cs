using KU2703056_SmartStay;

namespace SmartStayHotel;

// console menu for guests: registration, room search, reservations and cancellation
public class KU2703056_GuestMenu
{
    private readonly KU2703056_RegisterGuest registerGuest;
    private readonly KU2703056_SearchAvailableRooms searchAvailableRooms;
    private readonly KU2703056_MakeReservation makeReservation;
    private readonly KU2703056_SearchReservationByGuestName searchByName;
    private readonly KU2703056_SearchReservationByBookingId searchById;
    private readonly KU2703056_ViewReservationsByCheckIn viewByCheckIn;
    private readonly KU2703056_CancelReservation cancelReservation;
    private readonly KU2703056_CalculateFinalBill calculateFinalBill;
    private readonly KU2703056_GenerateInvoice generateInvoice;

    public KU2703056_GuestMenu(KU2703056_RegisterGuest registerGuest,
                               KU2703056_SearchAvailableRooms searchAvailableRooms,
                               KU2703056_MakeReservation makeReservation,
                               KU2703056_SearchReservationByGuestName searchByName,
                               KU2703056_SearchReservationByBookingId searchById,
                               KU2703056_ViewReservationsByCheckIn viewByCheckIn,
                               KU2703056_CancelReservation cancelReservation,
                               KU2703056_CalculateFinalBill calculateFinalBill,
                               KU2703056_GenerateInvoice generateInvoice)
    {
        this.registerGuest = registerGuest;
        this.searchAvailableRooms = searchAvailableRooms;
        this.makeReservation = makeReservation;
        this.searchByName = searchByName;
        this.searchById = searchById;
        this.viewByCheckIn = viewByCheckIn;
        this.cancelReservation = cancelReservation;
        this.calculateFinalBill = calculateFinalBill;
        this.generateInvoice = generateInvoice;
    }

    // shows the menu again and again until the user chooses 0 (Back)
    public void Run()
    {
        bool goBack = false;

        while (!goBack)
        {
            Console.WriteLine();
            Console.WriteLine("============== GUEST MENU ==============");
            Console.WriteLine("1. Register as a guest");
            Console.WriteLine("2. Search available rooms");
            Console.WriteLine("3. Make a reservation");
            Console.WriteLine("4. Search reservations by guest name");
            Console.WriteLine("5. Search reservation by booking ID");
            Console.WriteLine("6. View reservations by check-in date");
            Console.WriteLine("7. Cancel a reservation");
            Console.WriteLine("0. Back to main menu");

            int choice = KU2703056_ConsoleInput.ReadInt("Choose an option: ");

            switch (choice)
            {
                case 1: RegisterOption(); break;
                case 2: SearchRoomsOption(); break;
                case 3: MakeReservationOption(); break;
                case 4: SearchByNameOption(); break;
                case 5: SearchByIdOption(); break;
                case 6: ViewByCheckInOption(); break;
                case 7: CancelOption(); break;
                case 0: goBack = true; break;
                default: Console.WriteLine("Invalid option. Please choose from the menu."); break;
            }
        }
    }

    private void RegisterOption()
    {
        KU2703056_ConsoleInput.ShowTitle("GUEST REGISTRATION");
        string nic = KU2703056_ConsoleInput.ReadText("NIC/Passport number: ");
        string name = KU2703056_ConsoleInput.ReadText("Full name: ");
        string contact = KU2703056_ConsoleInput.ReadText("Contact number: ");
        string email = KU2703056_ConsoleInput.ReadText("Email address: ");

        registerGuest.Execute(nic, name, contact, email, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    private void SearchRoomsOption()
    {
        KU2703056_ConsoleInput.ShowTitle("SEARCH AVAILABLE ROOMS");
        KU2703056_RoomType roomType = KU2703056_ConsoleInput.ReadRoomType();
        DateTime checkIn = KU2703056_ConsoleInput.ReadDate("Check-in date");
        DateTime checkOut = KU2703056_ConsoleInput.ReadDate("Check-out date");

        if (checkOut.Date <= checkIn.Date)
        {
            Console.WriteLine("Check-out date must be after the check-in date.");
            KU2703056_ConsoleInput.Pause();
            return;
        }

        List<KU2703056_Room> rooms = searchAvailableRooms.Execute(roomType, checkIn, checkOut);

        if (rooms.Count == 0)
        {
            Console.WriteLine($"No {roomType} room is available for those dates.");
        }
        else
        {
            Console.WriteLine($"{rooms.Count} room(s) available:");
            Console.WriteLine("ROOM ID  TYPE         BED      DAILY RATE      STATUS");
            foreach (KU2703056_Room room in rooms)
            {
                Console.WriteLine(room);
            }
        }

        KU2703056_ConsoleInput.Pause();
    }

    private void MakeReservationOption()
    {
        KU2703056_ConsoleInput.ShowTitle("MAKE A RESERVATION");
        string nic = KU2703056_ConsoleInput.ReadText("Your NIC/Passport number: ");
        KU2703056_RoomType roomType = KU2703056_ConsoleInput.ReadRoomType();
        int guests = KU2703056_ConsoleInput.ReadInt("Number of guests: ");
        DateTime checkIn = KU2703056_ConsoleInput.ReadDate("Check-in date");
        DateTime checkOut = KU2703056_ConsoleInput.ReadDate("Check-out date");

        bool success = makeReservation.Execute(nic, roomType, guests, checkIn, checkOut,
                                               DateTime.Today, out KU2703056_Booking? booking,
                                               out string message);
        Console.WriteLine(message);

        // on success, show the invoice straight away
        if (success && booking != null)
        {
            Console.WriteLine();
            KU2703056_Invoice invoice = calculateFinalBill.Execute(booking);
            Console.WriteLine(generateInvoice.Execute(invoice));
        }

        KU2703056_ConsoleInput.Pause();
    }

    private void SearchByNameOption()
    {
        KU2703056_ConsoleInput.ShowTitle("SEARCH RESERVATIONS BY GUEST NAME");
        string name = KU2703056_ConsoleInput.ReadText("Guest name (full or part): ");

        List<KU2703056_Booking> results = searchByName.Execute(name);
        ShowBookingList(results);
        KU2703056_ConsoleInput.Pause();
    }

    private void SearchByIdOption()
    {
        KU2703056_ConsoleInput.ShowTitle("SEARCH RESERVATION BY BOOKING ID");
        string bookingId = KU2703056_ConsoleInput.ReadText("Booking ID (example BK001): ");

        KU2703056_Booking? booking = searchById.Execute(bookingId);

        if (booking == null)
        {
            Console.WriteLine("Booking ID was not found.");
        }
        else
        {
            Console.WriteLine(BookingLine(booking));

            // cancelled bookings have no bill; confirmed ones show the invoice
            if (booking.Status == KU2703056_BookingStatus.Confirmed)
            {
                Console.WriteLine();
                KU2703056_Invoice invoice = calculateFinalBill.Execute(booking);
                Console.WriteLine(generateInvoice.Execute(invoice));
            }
        }

        KU2703056_ConsoleInput.Pause();
    }

    private void ViewByCheckInOption()
    {
        KU2703056_ConsoleInput.ShowTitle("VIEW RESERVATIONS BY CHECK-IN DATE");
        DateTime? date = KU2703056_ConsoleInput.ReadOptionalDate("Check-in date");

        List<KU2703056_Booking> results = viewByCheckIn.Execute(date);
        ShowBookingList(results);
        KU2703056_ConsoleInput.Pause();
    }

    private void CancelOption()
    {
        KU2703056_ConsoleInput.ShowTitle("CANCEL A RESERVATION");
        string bookingId = KU2703056_ConsoleInput.ReadText("Booking ID to cancel: ");

        cancelReservation.Execute(bookingId, DateTime.Today, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    // prints a list of bookings, or a message if the list is empty
    private void ShowBookingList(List<KU2703056_Booking> bookings)
    {
        if (bookings.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            return;
        }

        Console.WriteLine($"{bookings.Count} reservation(s) found:");
        foreach (KU2703056_Booking booking in bookings)
        {
            Console.WriteLine(BookingLine(booking));
        }
    }

    // one-line summary of a booking
    private string BookingLine(KU2703056_Booking booking)
    {
        return $"{booking.BookingId} | {booking.Guest.FullName} | Room {booking.Room.RoomId} | " +
               $"{booking.CheckInDate:dd/MM/yyyy} to {booking.CheckOutDate:dd/MM/yyyy} | " +
               $"{booking.NumberOfGuests} guest(s) | {booking.Status}";
    }
}
