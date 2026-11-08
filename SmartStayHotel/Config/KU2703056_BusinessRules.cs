namespace KU2703056_SmartStay;

// holds the fixed rules
public static class KU2703056_BusinessRules
{
    public const decimal AdvancePayment = 10000m;      // refundable booking advance (LKR)
    public const int MinDaysBeforeCheckIn = 2;         // bookings must be made 2 days ahead
    public const int MinDaysToCancel = 2;              // assumption for the "24 hours" rule
    public const int DiscountMinNights = 5;            // discount starts at 5 nights
    public const decimal DiscountRate = 0.12m;         // 12% on the base room charge
}
