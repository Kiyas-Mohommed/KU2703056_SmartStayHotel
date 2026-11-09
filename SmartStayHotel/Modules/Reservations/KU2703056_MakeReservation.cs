using KU2703056_SmartStay;

namespace SmartStayHotel;

// guest operation: creates a new reservation and assigns a room automatically
public class KU2703056_MakeReservation
{
    private readonly KU2703056_GuestRepository guestRepository;
    private readonly KU2703056_BookingRepository bookingRepository;
    private readonly KU2703056_BookingValidator bookingValidator;
    private readonly KU2703056_SearchAvailableRooms searchAvailableRooms;

    public KU2703056_MakeReservation(KU2703056_GuestRepository guestRepository,
                                     KU2703056_BookingRepository bookingRepository,
                                     KU2703056_BookingValidator bookingValidator,
                                     KU2703056_SearchAvailableRooms searchAvailableRooms)
    {
        this.guestRepository = guestRepository;
        this.bookingRepository = bookingRepository;
        this.bookingValidator = bookingValidator;
        this.searchAvailableRooms = searchAvailableRooms;
    }

    // returns true and the new booking if successful
    // returns false and a message (booking stays null) if any rule fails
    public bool Execute(string nicOrPassport, KU2703056_RoomType roomType, int numberOfGuests,
                        DateTime checkIn, DateTime checkOut, DateTime today,
                        out KU2703056_Booking? booking, out string message)
    {
        booking = null;

        // rule 1: the guest must be registered
        KU2703056_Guest? guest = guestRepository.Find(nicOrPassport.Trim());
        if (guest == null)
        {
            message = "Guest is not registered. Please register first.";
            return false;
        }

        // rule 2: guests, dates and the 2-day advance rule must be valid
        if (!bookingValidator.IsValid(checkIn, checkOut, numberOfGuests, today, out string errorMessage))
        {
            message = errorMessage;
            return false;
        }

        // rule 3: find free rooms of the chosen type for these dates
        List<KU2703056_Room> freeRooms = searchAvailableRooms.Execute(roomType, checkIn, checkOut);
        if (freeRooms.Count == 0)
        {
            message = $"No {roomType} room is available for the selected dates.";
            return false;
        }

        // rule 4: auto-assign the first free room (lowest Room ID)
        KU2703056_Room assignedRoom = freeRooms[0];

        // create and save the booking (the LKR 10,000 advance is set inside Booking)
        string bookingId = bookingRepository.GenerateBookingId();
        booking = new KU2703056_Booking(bookingId, guest, assignedRoom, checkIn, checkOut, numberOfGuests);
        bookingRepository.Add(booking);

        message = $"Reservation confirmed. Booking ID: {bookingId}, Room: {assignedRoom.RoomId}.";
        return true;
    }
}
