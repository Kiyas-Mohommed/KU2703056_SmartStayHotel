namespace SmartStayHotel;

// guest operation: finds reservations by guest name (part of the name is enough)
public class KU2703056_SearchReservationByGuestName
{
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_SearchReservationByGuestName(KU2703056_BookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    // not case sensitive. returns an empty list if nothing matches
    public List<KU2703056_Booking> Execute(string guestName)
    {
        List<KU2703056_Booking> results = new List<KU2703056_Booking>();

        if (string.IsNullOrWhiteSpace(guestName))
        {
            return results;
        }

        foreach (KU2703056_Booking booking in bookingRepository.GetAll())
        {
            if (booking.Guest.FullName.Contains(guestName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                results.Add(booking);
            }
        }

        return results;
    }
}
