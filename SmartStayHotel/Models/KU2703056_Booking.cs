using KU2703056_SmartStay;

namespace SmartStayHotel;

// stores one room reservation and answers simple questions about it
public class KU2703056_Booking
{
    public string BookingId { get; private set; }
    public KU2703056_Guest Guest { get; private set; }
    public KU2703056_Room Room { get; private set; }
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    public int NumberOfGuests { get; private set; }
    public decimal AdvancePaid { get; private set; }
    public KU2703056_BookingStatus Status { get; private set; }

    public KU2703056_Booking(string bookingId, KU2703056_Guest guest, KU2703056_Room room,
                             DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
    {
        BookingId = bookingId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate.Date;
        CheckOutDate = checkOutDate.Date;
        NumberOfGuests = numberOfGuests;
        AdvancePaid = KU2703056_BusinessRules.AdvancePayment;
        Status = KU2703056_BookingStatus.Confirmed;
    }

    // number of nights stayed
    public int Nights
    {
        get { return (CheckOutDate - CheckInDate).Days; }
    }

    // true if the booking is active and check-in is far enough away to cancel
    public bool IsCancellable(DateTime today)
    {
        int daysUntilCheckIn = (CheckInDate - today.Date).Days;
        return Status == KU2703056_BookingStatus.Confirmed
               && daysUntilCheckIn >= KU2703056_BusinessRules.MinDaysToCancel;
    }

    // marks the booking as cancelled
    public void Cancel()
    {
        Status = KU2703056_BookingStatus.Cancelled;
    }

    // date overlap check: a cancelled booking never blocks a room
    // a new guest may check in on the day another guest checks out
    public bool OverlapsWith(DateTime newCheckIn, DateTime newCheckOut)
    {
        return Status == KU2703056_BookingStatus.Confirmed
               && newCheckIn.Date < CheckOutDate
               && newCheckOut.Date > CheckInDate;
    }
}
