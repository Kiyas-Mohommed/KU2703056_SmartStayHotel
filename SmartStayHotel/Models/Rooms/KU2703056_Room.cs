using KU2703056_SmartStay;

namespace SmartStayHotel;

// abstract base class for every room type
// common details are stored here, and each room type supplies its own rates
public abstract class KU2703056_Room
{
    public string RoomId { get; private set; }
    public KU2703056_RoomType RoomType { get; private set; }
    public KU2703056_BedType BedType { get; set; }
    public KU2703056_AvailabilityStatus Status { get; set; }

    // each room type must provide these values (polymorphism)
    public abstract decimal DailyRate { get; }
    public abstract decimal ExtraGuestRate { get; }
    public abstract decimal TaxRate { get; }
    public abstract int IncludedGuests { get; }
    public abstract bool HasFreeBreakfast { get; }

    protected KU2703056_Room(string roomId, KU2703056_RoomType roomType, KU2703056_BedType bedType)
    {
        RoomId = roomId;
        RoomType = roomType;
        BedType = bedType;
        Status = KU2703056_AvailabilityStatus.Available;
    }

    // one-line description used when listing rooms
    public override string ToString()
    {
        return $"{RoomId,-8} {RoomType,-12} {BedType,-8} LKR {DailyRate,8:N2}  {Status}";
    }
}
