namespace SmartStayHotel;

// guest operation: registers a new guest after checking the details
public class KU2703056_RegisterGuest
{
    private readonly KU2703056_GuestRepository guestRepository;
    private readonly KU2703056_GuestValidator guestValidator;

    public KU2703056_RegisterGuest(KU2703056_GuestRepository guestRepository,
                                   KU2703056_GuestValidator guestValidator)
    {
        this.guestRepository = guestRepository;
        this.guestValidator = guestValidator;
    }

    public bool Execute(string nicOrPassport, string fullName, string contactNumber,
                        string email, out string message)
    {
        // step 1: check the details are valid
        if (!guestValidator.IsValid(nicOrPassport, fullName, contactNumber, email, out string errorMessage))
        {
            message = errorMessage;
            return false;
        }

        // step 2: create the guest (spaces trimmed) and try to save it
        KU2703056_Guest guest = new KU2703056_Guest(nicOrPassport.Trim(), fullName.Trim(),
                                                    contactNumber.Trim(), email.Trim());

        if (!guestRepository.Add(guest))
        {
            message = $"A guest with ID {guest.NicOrPassport} is already registered.";
            return false;
        }

        message = $"Guest {guest.FullName} was registered successfully.";
        return true;
    }
}
