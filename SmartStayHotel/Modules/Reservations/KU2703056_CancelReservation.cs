using KU2703056_SmartStay;

namespace SmartStayHotel;

// guest operation: cancels a reservation if check-in is 2 or more days away
// a cancelled reservation gets its LKR 10,000 advance refunded in full
public class KU2703056_CancelReservation
{
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_CancelReservation(KU2703056_BookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    public bool Execute(string bookingId, DateTime today, out string message)
    {
        KU2703056_Booking? booking = string.IsNullOrWhiteSpace(bookingId)
            ? null
            : bookingRepository.FindById(bookingId.Trim());

        // check 1: the booking must exist
        if (booking == null)
        {
            message = "Booking ID was not found.";
            return false;
        }

        // check 2: it must not already be cancelled
        if (booking.Status == KU2703056_BookingStatus.Cancelled)
        {
            message = $"Booking {booking.BookingId} is already cancelled.";
            return false;
        }

        // check 3: cancellation is not allowed within 24 hours of check-in
        if (!booking.IsCancellable(today))
        {
            message = $"Booking {booking.BookingId} cannot be cancelled. Check-in is too close.";
            return false;
        }

        booking.Cancel();
        message = $"Booking {booking.BookingId} was cancelled. Advance of LKR {booking.AdvancePaid:N2} will be refunded.";
        return true;
    }
}
