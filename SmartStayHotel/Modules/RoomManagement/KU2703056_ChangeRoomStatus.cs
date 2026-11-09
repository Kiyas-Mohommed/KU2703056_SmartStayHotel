using KU2703056_SmartStay;

namespace SmartStayHotel;

// Administrator operation: sets a room to Available, Reserved or Under Maintenance.
public class KU2703056_ChangeRoomStatus
{
    private readonly KU2703056_RoomRepository roomRepository;

    public KU2703056_ChangeRoomStatus(KU2703056_RoomRepository roomRepository)
    {
        this.roomRepository = roomRepository;
    }

    public bool Execute(string roomId, KU2703056_AvailabilityStatus newStatus, out string message)
    {
        KU2703056_Room? room = roomRepository.Find(roomId);

        if (room == null)
        {
            message = $"Room {roomId} was not found.";
            return false;
        }

        room.Status = newStatus;
        message = $"Room {room.RoomId} status changed to {newStatus}.";
        return true;
    }
}
