using KU2703056_SmartStay;

namespace SmartStayHotel;

// creates the correct room object for a given room type (Factory pattern)
public static class KU2703056_RoomFactory
{
    public static KU2703056_Room Create(string roomId, KU2703056_RoomType roomType, KU2703056_BedType bedType)
    {
        switch (roomType)
        {
            case KU2703056_RoomType.Standard:
                return new KU2703056_StandardRoom(roomId, bedType);
            case KU2703056_RoomType.Deluxe:
                return new KU2703056_DeluxeRoom(roomId, bedType);
            case KU2703056_RoomType.Suite:
                return new KU2703056_SuiteRoom(roomId, bedType);
            case KU2703056_RoomType.FamilySuite:
                return new KU2703056_FamilySuiteRoom(roomId, bedType);
            default:
                throw new ArgumentException("Unknown room type.");
        }
    }
}
