namespace SmartStayHotel;

// guest operation: finds one reservation by its booking ID (example: BK001)
public class KU2703056_SearchReservationByBookingId
{
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_SearchReservationByBookingId(KU2703056_BookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    // returns the booking, or null if the ID does not exist
    public KU2703056_Booking? Execute(string bookingId)
    {
        if (string.IsNullOrWhiteSpace(bookingId))
        {
            return null;
        }

        return bookingRepository.FindById(bookingId.Trim());
    }
}
