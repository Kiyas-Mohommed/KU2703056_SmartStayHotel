using KU2703056_SmartStay;

namespace SmartStayHotel;

// checks reservation details before a booking is created
public class KU2703056_BookingValidator
{
    // 'today' is passed in (instead of read inside) so boundary cases are easy to test
    public bool IsValid(DateTime checkIn, DateTime checkOut, int numberOfGuests,
                        DateTime today, out string errorMessage)
    {
        if (numberOfGuests < 1)
        {
            errorMessage = "Number of guests must be at least 1.";
            return false;
        }

        if (checkOut.Date <= checkIn.Date)
        {
            errorMessage = "Check-out date must be after the check-in date.";
            return false;
        }

        int daysAhead = (checkIn.Date - today.Date).Days;
        if (daysAhead < KU2703056_BusinessRules.MinDaysBeforeCheckIn)
        {
            errorMessage = $"Bookings must be made at least {KU2703056_BusinessRules.MinDaysBeforeCheckIn} days before check-in.";
            return false;
        }

        errorMessage = "";
        return true;
    }
}
