namespace SmartStayHotel;

// stores all rooms in memory. room ID is the key (not case sensitive)
public class KU2703056_RoomRepository
{
    private readonly Dictionary<string, KU2703056_Room> rooms =
        new Dictionary<string, KU2703056_Room>(StringComparer.OrdinalIgnoreCase);

    // adds a room. returns false if the Room ID already exists
    public bool Add(KU2703056_Room room)
    {
        if (rooms.ContainsKey(room.RoomId))
        {
            return false;
        }
        rooms.Add(room.RoomId, room);
        return true;
    }

    // removes a room. returns false if the Room ID was not found
    public bool Remove(string roomId)
    {
        return rooms.Remove(roomId);
    }

    // returns the room, or null if it does not exist
    public KU2703056_Room? Find(string roomId)
    {
        rooms.TryGetValue(roomId, out KU2703056_Room? room);
        return room;
    }

    public bool Exists(string roomId)
    {
        return rooms.ContainsKey(roomId);
    }

    // returns a copy of all rooms as a list
    public List<KU2703056_Room> GetAll()
    {
        return rooms.Values.ToList();
    }
}
