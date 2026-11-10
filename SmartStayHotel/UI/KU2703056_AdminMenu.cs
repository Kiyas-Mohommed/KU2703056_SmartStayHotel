using KU2703056_SmartStay;

namespace SmartStayHotel;

// console menu for the hotel administrator (room management)
public class KU2703056_AdminMenu
{
    private readonly KU2703056_AddRoom addRoom;
    private readonly KU2703056_UpdateRoom updateRoom;
    private readonly KU2703056_RemoveRoom removeRoom;
    private readonly KU2703056_ChangeRoomStatus changeRoomStatus;
    private readonly KU2703056_ViewAllRooms viewAllRooms;

    public KU2703056_AdminMenu(KU2703056_AddRoom addRoom,
                               KU2703056_UpdateRoom updateRoom,
                               KU2703056_RemoveRoom removeRoom,
                               KU2703056_ChangeRoomStatus changeRoomStatus,
                               KU2703056_ViewAllRooms viewAllRooms)
    {
        this.addRoom = addRoom;
        this.updateRoom = updateRoom;
        this.removeRoom = removeRoom;
        this.changeRoomStatus = changeRoomStatus;
        this.viewAllRooms = viewAllRooms;
    }

    // shows the menu again and again until the user chooses 0 (Back)
    public void Run()
    {
        bool goBack = false;

        while (!goBack)
        {
            Console.WriteLine();
            Console.WriteLine("========== ADMINISTRATOR MENU ==========");
            Console.WriteLine("1. Add room");
            Console.WriteLine("2. Update room");
            Console.WriteLine("3. Remove room");
            Console.WriteLine("4. Change room availability status");
            Console.WriteLine("5. View all rooms");
            Console.WriteLine("0. Back to main menu");

            int choice = KU2703056_ConsoleInput.ReadInt("Choose an option: ");

            switch (choice)
            {
                case 1: AddRoomOption(); break;
                case 2: UpdateRoomOption(); break;
                case 3: RemoveRoomOption(); break;
                case 4: ChangeStatusOption(); break;
                case 5: ViewAllRoomsOption(); break;
                case 0: goBack = true; break;
                default: Console.WriteLine("Invalid option. Please choose from the menu."); break;
            }
        }
    }

    private void AddRoomOption()
    {
        KU2703056_ConsoleInput.ShowTitle("ADD ROOM");
        string roomId = KU2703056_ConsoleInput.ReadText("Room ID (example STD103): ");
        KU2703056_RoomType roomType = KU2703056_ConsoleInput.ReadRoomType();
        KU2703056_BedType bedType = KU2703056_ConsoleInput.ReadBedType();

        addRoom.Execute(roomId, roomType, bedType, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    private void UpdateRoomOption()
    {
        KU2703056_ConsoleInput.ShowTitle("UPDATE ROOM");
        string roomId = KU2703056_ConsoleInput.ReadText("Room ID to update: ");
        KU2703056_RoomType roomType = KU2703056_ConsoleInput.ReadRoomType();
        KU2703056_BedType bedType = KU2703056_ConsoleInput.ReadBedType();

        updateRoom.Execute(roomId, roomType, bedType, DateTime.Today, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    private void RemoveRoomOption()
    {
        KU2703056_ConsoleInput.ShowTitle("REMOVE ROOM");
        string roomId = KU2703056_ConsoleInput.ReadText("Room ID to remove: ");

        removeRoom.Execute(roomId, DateTime.Today, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    private void ChangeStatusOption()
    {
        KU2703056_ConsoleInput.ShowTitle("CHANGE ROOM STATUS");
        string roomId = KU2703056_ConsoleInput.ReadText("Room ID: ");
        KU2703056_AvailabilityStatus status = KU2703056_ConsoleInput.ReadStatus();

        changeRoomStatus.Execute(roomId, status, out string message);
        Console.WriteLine(message);
        KU2703056_ConsoleInput.Pause();
    }

    private void ViewAllRoomsOption()
    {
        KU2703056_ConsoleInput.ShowTitle("ALL ROOMS");
        List<KU2703056_Room> rooms = viewAllRooms.Execute();

        if (rooms.Count == 0)
        {
            Console.WriteLine("No rooms have been added yet.");
        }
        else
        {
            Console.WriteLine("ROOM ID  TYPE         BED      DAILY RATE      STATUS");
            foreach (KU2703056_Room room in rooms)
            {
                Console.WriteLine(room);
            }
        }

        KU2703056_ConsoleInput.Pause();
    }
}
