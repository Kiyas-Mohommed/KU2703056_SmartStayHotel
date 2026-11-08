using KU2703056_SmartStay;

namespace SmartStayHotel;

// deluxe room: LKR 9,000 per night, free breakfast
public class KU2703056_DeluxeRoom : KU2703056_Room
{
    public KU2703056_DeluxeRoom(string roomId, KU2703056_BedType bedType)
        : base(roomId, KU2703056_RoomType.Deluxe, bedType) { }

    public override decimal DailyRate => 9000m;
    public override decimal ExtraGuestRate => 1500m;
    public override decimal TaxRate => 0.12m;
    public override int IncludedGuests => 2;
    public override bool HasFreeBreakfast => true;
}
