namespace SmartStayHotel;

// stores the details of one registered guest
public class KU2703056_Guest
{
    public string NicOrPassport { get; private set; }
    public string FullName { get; private set; }
    public string ContactNumber { get; private set; }
    public string Email { get; private set; }

    public KU2703056_Guest(string nicOrPassport, string fullName, string contactNumber, string email)
    {
        NicOrPassport = nicOrPassport;
        FullName = fullName;
        ContactNumber = contactNumber;
        Email = email;
    }

    public override string ToString()
    {
        return $"{FullName} (ID: {NicOrPassport}) | {ContactNumber} | {Email}";
    }
}
