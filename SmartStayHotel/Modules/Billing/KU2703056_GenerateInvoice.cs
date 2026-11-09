using System.Text;

namespace SmartStayHotel;

// billing operation: turns an Invoice into formatted text for display
public class KU2703056_GenerateInvoice
{
    public string Execute(KU2703056_Invoice invoice)
    {
        KU2703056_Booking booking = invoice.Booking;
        KU2703056_Room room = booking.Room;
        KU2703056_Guest guest = booking.Guest;

        string doubleLine = new string('=', 52);
        string singleLine = new string('-', 52);
        StringBuilder text = new StringBuilder();

        text.AppendLine(doubleLine);
        text.AppendLine("              SMARTSTAY HOTEL - INVOICE");
        text.AppendLine(doubleLine);
        text.AppendLine($"Booking ID : {booking.BookingId}");
        text.AppendLine();

        text.AppendLine("GUEST DETAILS");
        text.AppendLine($"Name       : {guest.FullName}");
        text.AppendLine($"NIC/Passport: {guest.NicOrPassport}");
        text.AppendLine($"Contact    : {guest.ContactNumber}");
        text.AppendLine($"Email      : {guest.Email}");
        text.AppendLine();

        text.AppendLine("ROOM DETAILS");
        text.AppendLine($"Room ID    : {room.RoomId}");
        text.AppendLine($"Room Type  : {room.RoomType}");
        text.AppendLine($"Bed Type   : {room.BedType}");
        text.AppendLine($"Daily Rate : LKR {room.DailyRate:N2}");
        text.AppendLine();

        text.AppendLine("STAY DETAILS");
        text.AppendLine($"Check-in   : {booking.CheckInDate:dd MMM yyyy}");
        text.AppendLine($"Check-out  : {booking.CheckOutDate:dd MMM yyyy}");
        text.AppendLine($"Nights     : {booking.Nights}");
        text.AppendLine($"Guests     : {booking.NumberOfGuests} (included in rate: {room.IncludedGuests})");
        text.AppendLine(singleLine);

        text.AppendLine(Line("Base room charge", invoice.BaseCharge));
        text.AppendLine(Line("Extra guest charges", invoice.ExtraGuestCharge));
        text.AppendLine(Line("Less: Multi-night discount", invoice.Discount));
        text.AppendLine(singleLine);
        text.AppendLine(Line("Subtotal", invoice.Subtotal));
        text.AppendLine(Line($"Tax ({room.TaxRate * 100:0}%)", invoice.Tax));
        text.AppendLine(Line("Less: Advance paid", invoice.AdvanceDeducted));
        text.AppendLine(doubleLine);

        // a short stay can cost less than the advance, so the guest gets money back
        if (invoice.FinalAmount >= 0)
        {
            text.AppendLine(Line("FINAL PAYABLE AMOUNT", invoice.FinalAmount));
        }
        else
        {
            text.AppendLine(Line("REFUND DUE TO GUEST", Math.Abs(invoice.FinalAmount)));
        }

        text.AppendLine(doubleLine);
        return text.ToString();
    }

    // formats one money line so all amounts line up in a column
    private string Line(string label, decimal amount)
    {
        return $"{label,-32}LKR {amount,16:N2}";
    }
}
