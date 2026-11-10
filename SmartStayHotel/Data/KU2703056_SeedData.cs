using KU2703056_SmartStay;

namespace SmartStayHotel;

// loads sample rooms when the program starts (data is kept in memory only)
public static class KU2703056_SeedData
{
    public static void Load(KU2703056_RoomRepository roomRepository)
    {
        roomRepository.Add(KU2703056_RoomFactory.Create("STD101", KU2703056_RoomType.Standard, KU2703056_BedType.Single));
        roomRepository.Add(KU2703056_RoomFactory.Create("STD102", KU2703056_RoomType.Standard, KU2703056_BedType.Double));
        roomRepository.Add(KU2703056_RoomFactory.Create("DLX201", KU2703056_RoomType.Deluxe, KU2703056_BedType.Queen));
        roomRepository.Add(KU2703056_RoomFactory.Create("DLX202", KU2703056_RoomType.Deluxe, KU2703056_BedType.King));
        roomRepository.Add(KU2703056_RoomFactory.Create("STE301", KU2703056_RoomType.Suite, KU2703056_BedType.King));
        roomRepository.Add(KU2703056_RoomFactory.Create("FAM401", KU2703056_RoomType.FamilySuite, KU2703056_BedType.Queen));
    }
}
