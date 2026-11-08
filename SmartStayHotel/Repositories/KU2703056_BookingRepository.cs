using KU2703056_SmartStay;

namespace SmartStayHotel;

// stores all bookings in memory and creates new booking IDs
public class KU2703056_BookingRepository
{
    private readonly List<KU2703056_Booking> bookings = new List<KU2703056_Booking>();
    private int lastBookingNumber = 0;

    // creates the next booking ID: BK001, BK002, BK003...
    public string GenerateBookingId()
    {
        lastBookingNumber++;
        return "BK" + lastBookingNumber.ToString("D3");
    }

    public void Add(KU2703056_Booking booking)
    {
        bookings.Add(booking);
    }

    // returns the booking, or null if the ID does not exist
    public KU2703056_Booking? FindById(string bookingId)
    {
        foreach (KU2703056_Booking booking in bookings)
        {
            if (string.Equals(booking.BookingId, bookingId, StringComparison.OrdinalIgnoreCase))
            {
                return booking;
            }
        }
        return null;
    }

    // returns a copy of all bookings as a list
    public List<KU2703056_Booking> GetAll()
    {
        return new List<KU2703056_Booking>(bookings);
    }

    // true if the room has a confirmed booking whose stay has not finished yet
    public bool HasActiveBooking(string roomId, DateTime today)
    {
        foreach (KU2703056_Booking booking in bookings)
        {
            if (booking.Status == KU2703056_BookingStatus.Confirmed
                && string.Equals(booking.Room.RoomId, roomId, StringComparison.OrdinalIgnoreCase)
                && booking.CheckOutDate > today.Date)
            {
                return true;
            }
        }
        return false;
    }
}
