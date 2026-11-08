using KU2703056_SmartStay;

namespace SmartStayHotel;

// family Suite: LKR 18,000 per night, free breakfast
public class KU2703056_FamilySuiteRoom : KU2703056_Room
{
    public KU2703056_FamilySuiteRoom(string roomId, KU2703056_BedType bedType)
        : base(roomId, KU2703056_RoomType.FamilySuite, bedType) { }

    public override decimal DailyRate => 18000m;
    public override decimal ExtraGuestRate => 2500m;
    public override decimal TaxRate => 0.15m;
    public override int IncludedGuests => 4;
    public override bool HasFreeBreakfast => true;
}
