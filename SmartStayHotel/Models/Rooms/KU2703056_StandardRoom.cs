using KU2703056_SmartStay;

namespace SmartStayHotel;

// standard room: LKR 6,000 per night, no free breakfast
public class KU2703056_StandardRoom : KU2703056_Room
{
    public KU2703056_StandardRoom(string roomId, KU2703056_BedType bedType)
        : base(roomId, KU2703056_RoomType.Standard, bedType) { }

    public override decimal DailyRate => 6000m;
    public override decimal ExtraGuestRate => 1000m;
    public override decimal TaxRate => 0.10m;
    public override int IncludedGuests => 2;
    public override bool HasFreeBreakfast => false;
}
