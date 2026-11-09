namespace SmartStayHotel;

// administrator operation: returns every room, sorted by Room ID
public class KU2703056_ViewAllRooms
{
    private readonly KU2703056_RoomRepository roomRepository;

    public KU2703056_ViewAllRooms(KU2703056_RoomRepository roomRepository)
    {
        this.roomRepository = roomRepository;
    }

    public List<KU2703056_Room> Execute()
    {
        return roomRepository.GetAll().OrderBy(room => room.RoomId).ToList();
    }
}
