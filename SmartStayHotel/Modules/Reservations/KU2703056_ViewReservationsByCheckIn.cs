namespace SmartStayHotel;

// guest operation: lists reservations ordered by check-in date (earliest first)
// if a date is given, only reservations checking in on that date are listed
public class KU2703056_ViewReservationsByCheckIn
{
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_ViewReservationsByCheckIn(KU2703056_BookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    // pass null to see all reservations
    public List<KU2703056_Booking> Execute(DateTime? checkInDate)
    {
        List<KU2703056_Booking> results = bookingRepository.GetAll();

        if (checkInDate.HasValue)
        {
            results = results.Where(booking => booking.CheckInDate == checkInDate.Value.Date).ToList();
        }

        // sort by check-in date, then by booking ID
        return results.OrderBy(booking => booking.CheckInDate)
                      .ThenBy(booking => booking.BookingId)
                      .ToList();
    }
}
