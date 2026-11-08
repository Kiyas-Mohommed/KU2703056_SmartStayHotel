namespace SmartStayHotel;

// stores all registered guests. NIC/Passport is the key, so it cannot be duplicated
public class KU2703056_GuestRepository
{
    private readonly Dictionary<string, KU2703056_Guest> guests =
        new Dictionary<string, KU2703056_Guest>(StringComparer.OrdinalIgnoreCase);

    // adds a guest. Returns false if the NIC/Passport is already registered
    public bool Add(KU2703056_Guest guest)
    {
        if (guests.ContainsKey(guest.NicOrPassport))
        {
            return false;
        }
        guests.Add(guest.NicOrPassport, guest);
        return true;
    }

    // returns the guest, or null if not registered
    public KU2703056_Guest? Find(string nicOrPassport)
    {
        guests.TryGetValue(nicOrPassport, out KU2703056_Guest? guest);
        return guest;
    }

    public bool Exists(string nicOrPassport)
    {
        return guests.ContainsKey(nicOrPassport);
    }
}
