using KU2703056_SmartStay;

namespace SmartStayHotel;

// administrator operation: changes a room's type and bed type
// the room type cannot change while the room has an active booking
public class KU2703056_UpdateRoom
{
    private readonly KU2703056_RoomRepository roomRepository;
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_UpdateRoom(KU2703056_RoomRepository roomRepository,
                                KU2703056_BookingRepository bookingRepository)
    {
        this.roomRepository = roomRepository;
        this.bookingRepository = bookingRepository;
    }

    public bool Execute(string roomId, KU2703056_RoomType newRoomType,
                        KU2703056_BedType newBedType, DateTime today, out string message)
    {
        KU2703056_Room? room = roomRepository.Find(roomId);

        if (room == null)
        {
            message = $"Room {roomId} was not found.";
            return false;
        }

        if (newRoomType == room.RoomType)
        {
            // same type: only the bed type changes
            room.BedType = newBedType;
        }
        else
        {
            if (bookingRepository.HasActiveBooking(room.RoomId, today))
            {
                message = $"Room {room.RoomId} has an active booking, so its room type cannot be changed.";
                return false;
            }

            // a different type needs a new room object (rates come from the type).
            KU2703056_Room newRoom = KU2703056_RoomFactory.Create(room.RoomId, newRoomType, newBedType);
            newRoom.Status = room.Status;
            roomRepository.Remove(room.RoomId);
            roomRepository.Add(newRoom);
        }

        message = $"Room {roomId} was updated successfully.";
        return true;
    }
}
