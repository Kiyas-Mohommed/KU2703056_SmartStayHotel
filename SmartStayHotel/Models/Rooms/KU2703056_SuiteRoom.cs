using KU2703056_SmartStay;

namespace SmartStayHotel;

// suite: LKR 14,000 per night, free breakfast
public class KU2703056_SuiteRoom : KU2703056_Room
{
    public KU2703056_SuiteRoom(string roomId, KU2703056_BedType bedType)
        : base(roomId, KU2703056_RoomType.Suite, bedType) { }

    public override decimal DailyRate => 14000m;
    public override decimal ExtraGuestRate => 2000m;
    public override decimal TaxRate => 0.15m;
    public override int IncludedGuests => 3;
    public override bool HasFreeBreakfast => true;
}
