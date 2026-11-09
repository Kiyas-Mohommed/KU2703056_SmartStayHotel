using KU2703056_SmartStay;

namespace SmartStayHotel;

// billing operation: works out every amount for a booking
public class KU2703056_CalculateFinalBill
{
    public KU2703056_Invoice Execute(KU2703056_Booking booking)
    {
        KU2703056_Room room = booking.Room;
        int nights = booking.Nights;

        // step 1: base room charge
        decimal baseCharge = room.DailyRate * nights;

        // step 2: extra guest charge (per extra guest, per night)
        int extraGuests = Math.Max(0, booking.NumberOfGuests - room.IncludedGuests);
        decimal extraGuestCharge = extraGuests * room.ExtraGuestRate * nights;

        // step 3: 12% discount on the BASE charge only, for 5 or more nights
        decimal discount = 0m;
        if (nights >= KU2703056_BusinessRules.DiscountMinNights)
        {
            discount = Math.Round(baseCharge * KU2703056_BusinessRules.DiscountRate, 2);
        }

        // step 4: tax is applied to (base + extra - discount)
        decimal subtotal = baseCharge + extraGuestCharge - discount;
        decimal tax = Math.Round(subtotal * room.TaxRate, 2);

        // step 5: deduct the advance already paid
        decimal advanceDeducted = booking.AdvancePaid;
        decimal finalAmount = subtotal + tax - advanceDeducted;

        return new KU2703056_Invoice(booking, baseCharge, extraGuestCharge, discount,
                                     subtotal, tax, advanceDeducted, finalAmount);
    }
}
