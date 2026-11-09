using KU2703056_SmartStay;

namespace SmartStayHotel;

// administrator operation: adds a new room
public class KU2703056_AddRoom
{
    private readonly KU2703056_RoomRepository roomRepository;

    public KU2703056_AddRoom(KU2703056_RoomRepository roomRepository)
    {
        this.roomRepository = roomRepository;
    }

    public bool Execute(string roomId, KU2703056_RoomType roomType,
                        KU2703056_BedType bedType, out string message)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            message = "Room ID cannot be empty.";
            return false;
        }

        roomId = roomId.Trim();
        KU2703056_Room room = KU2703056_RoomFactory.Create(roomId, roomType, bedType);

        if (!roomRepository.Add(room))
        {
            message = $"Room {roomId} already exists. Room ID must be unique.";
            return false;
        }

        message = $"Room {roomId} was added successfully.";
        return true;
    }
}
