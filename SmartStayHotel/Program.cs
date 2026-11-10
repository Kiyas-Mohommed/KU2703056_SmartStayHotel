namespace SmartStayHotel;

// entry point: creates all the objects, connects them together and starts the main menu
internal class Program
{
    static void Main(string[] args)
    {
        // 1. storage (in memory only)
        KU2703056_RoomRepository roomRepository = new KU2703056_RoomRepository();
        KU2703056_GuestRepository guestRepository = new KU2703056_GuestRepository();
        KU2703056_BookingRepository bookingRepository = new KU2703056_BookingRepository();

        // 2. sample rooms so the system has data to work with
        KU2703056_SeedData.Load(roomRepository);

        // 3. validators
        KU2703056_GuestValidator guestValidator = new KU2703056_GuestValidator();
        KU2703056_BookingValidator bookingValidator = new KU2703056_BookingValidator();

        // 4. operations
        KU2703056_SearchAvailableRooms searchAvailableRooms =
            new KU2703056_SearchAvailableRooms(roomRepository, bookingRepository);

        KU2703056_AddRoom addRoom = new KU2703056_AddRoom(roomRepository);
        KU2703056_UpdateRoom updateRoom = new KU2703056_UpdateRoom(roomRepository, bookingRepository);
        KU2703056_RemoveRoom removeRoom = new KU2703056_RemoveRoom(roomRepository, bookingRepository);
        KU2703056_ChangeRoomStatus changeRoomStatus = new KU2703056_ChangeRoomStatus(roomRepository);
        KU2703056_ViewAllRooms viewAllRooms = new KU2703056_ViewAllRooms(roomRepository);

        KU2703056_RegisterGuest registerGuest = new KU2703056_RegisterGuest(guestRepository, guestValidator);
        KU2703056_MakeReservation makeReservation = new KU2703056_MakeReservation(
            guestRepository, bookingRepository, bookingValidator, searchAvailableRooms);
        KU2703056_SearchReservationByGuestName searchByName =
            new KU2703056_SearchReservationByGuestName(bookingRepository);
        KU2703056_SearchReservationByBookingId searchById =
            new KU2703056_SearchReservationByBookingId(bookingRepository);
        KU2703056_ViewReservationsByCheckIn viewByCheckIn =
            new KU2703056_ViewReservationsByCheckIn(bookingRepository);
        KU2703056_CancelReservation cancelReservation =
            new KU2703056_CancelReservation(bookingRepository);

        KU2703056_CalculateFinalBill calculateFinalBill = new KU2703056_CalculateFinalBill();
        KU2703056_GenerateInvoice generateInvoice = new KU2703056_GenerateInvoice();

        // 5. menus
        KU2703056_AdminMenu adminMenu = new KU2703056_AdminMenu(
            addRoom, updateRoom, removeRoom, changeRoomStatus, viewAllRooms);
        KU2703056_GuestMenu guestMenu = new KU2703056_GuestMenu(
            registerGuest, searchAvailableRooms, makeReservation, searchByName,
            searchById, viewByCheckIn, cancelReservation, calculateFinalBill, generateInvoice);
        KU2703056_MainMenu mainMenu = new KU2703056_MainMenu(adminMenu, guestMenu);

        // 6. start the application
        mainMenu.Run();
    }
}
