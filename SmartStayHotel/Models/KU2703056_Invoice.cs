namespace SmartStayHotel;

// stores the calculated amounts for one booking
// the calculation itself is done later by the billing module
public class KU2703056_Invoice
{
    public KU2703056_Booking Booking { get; private set; }
    public decimal BaseCharge { get; private set; }
    public decimal ExtraGuestCharge { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Tax { get; private set; }
    public decimal AdvanceDeducted { get; private set; }
    public decimal FinalAmount { get; private set; }

    public KU2703056_Invoice(KU2703056_Booking booking, decimal baseCharge, decimal extraGuestCharge,
                             decimal discount, decimal subtotal, decimal tax,
                             decimal advanceDeducted, decimal finalAmount)
    {
        Booking = booking;
        BaseCharge = baseCharge;
        ExtraGuestCharge = extraGuestCharge;
        Discount = discount;
        Subtotal = subtotal;
        Tax = tax;
        AdvanceDeducted = advanceDeducted;
        FinalAmount = finalAmount;
    }
}
