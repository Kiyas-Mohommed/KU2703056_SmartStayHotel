using KU2703056_SmartStay;

namespace SmartStayHotel;

// guest operation: finds rooms of a given type that are free for the requested dates
// a room is free when its status is Available AND no confirmed booking overlaps the dates
public class KU2703056_SearchAvailableRooms
{
    private readonly KU2703056_RoomRepository roomRepository;
    private readonly KU2703056_BookingRepository bookingRepository;

    public KU2703056_SearchAvailableRooms(KU2703056_RoomRepository roomRepository,
                                          KU2703056_BookingRepository bookingRepository)
    {
        this.roomRepository = roomRepository;
        this.bookingRepository = bookingRepository;
    }

    // returns the free rooms, sorted by Room ID (empty list if none)
    public List<KU2703056_Room> Execute(KU2703056_RoomType roomType, DateTime checkIn, DateTime checkOut)
    {
        List<KU2703056_Room> freeRooms = new List<KU2703056_Room>();

        // invalid date range: nothing can be booked
        if (checkOut.Date <= checkIn.Date)
        {
            return freeRooms;
        }

        List<KU2703056_Booking> allBookings = bookingRepository.GetAll();

        foreach (KU2703056_Room room in roomRepository.GetAll())
        {
            // rule 1: right room type and status must be Available
            if (room.RoomType != roomType || room.Status != KU2703056_AvailabilityStatus.Available)
            {
                continue;
            }

            // rule 2: no existing booking for this room may overlap the dates
            bool isFree = true;
            foreach (KU2703056_Booking booking in allBookings)
            {
                if (booking.Room.RoomId == room.RoomId && booking.OverlapsWith(checkIn, checkOut))
                {
                    isFree = false;
                    break;
                }
            }

            if (isFree)
            {
                freeRooms.Add(room);
            }
        }

        return freeRooms.OrderBy(room => room.RoomId).ToList();
    }
}
