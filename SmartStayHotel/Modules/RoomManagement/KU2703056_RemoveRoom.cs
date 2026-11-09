namespace SmartStayHotel;

// administrator operation: removes a room that has no active booking
public class KU2703056_RemoveRoom
{
    private readonly KU2703056_RoomRepository roomRepository;
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_RemoveRoom(KU2703056_RoomRepository roomRepository,
                                KU2703056_BookingRepository bookingRepository)
    {
        this.roomRepository = roomRepository;
        this.bookingRepository = bookingRepository;
    }

    public bool Execute(string roomId, DateTime today, out string message)
    {
        if (!roomRepository.Exists(roomId))
        {
            message = $"Room {roomId} was not found.";
            return false;
        }

        if (bookingRepository.HasActiveBooking(roomId, today))
        {
            message = $"Room {roomId} has an active booking and cannot be removed.";
            return false;
        }

        roomRepository.Remove(roomId);
        message = $"Room {roomId} was removed successfully.";
        return true;
    }
}
