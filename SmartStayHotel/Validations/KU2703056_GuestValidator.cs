namespace SmartStayHotel;

// checks guest registration details before a guest is saved
public class KU2703056_GuestValidator
{
    // returns true if all details are valid
    // if not, errorMessage explains the first problem found
    public bool IsValid(string nicOrPassport, string fullName, string contactNumber,
                        string email, out string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(nicOrPassport))
        {
            errorMessage = "NIC/Passport number cannot be empty.";
            return false;
        }

        if (!nicOrPassport.Trim().All(char.IsLetterOrDigit))
        {
            errorMessage = "NIC/Passport number can contain only letters and digits.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            errorMessage = "Full name cannot be empty.";
            return false;
        }

        if (!IsValidContactNumber(contactNumber))
        {
            errorMessage = "Contact number must have 9 to 12 digits (a leading + is allowed).";
            return false;
        }

        if (!IsValidEmail(email))
        {
            errorMessage = "Email address is not valid (example: name@example.com).";
            return false;
        }

        errorMessage = "";
        return true;
    }

    // contact number: optional leading +, then 9 to 12 digits
    private bool IsValidContactNumber(string contactNumber)
    {
        if (string.IsNullOrWhiteSpace(contactNumber))
        {
            return false;
        }

        string digits = contactNumber.Trim().TrimStart('+');
        return digits.Length >= 9 && digits.Length <= 12 && digits.All(char.IsDigit);
    }

    // email: one @, something before it, and a dot after it
    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        string text = email.Trim();
        int atPosition = text.IndexOf('@');

        return atPosition > 0
               && atPosition == text.LastIndexOf('@')
               && !text.Contains(' ')
               && text.IndexOf('.', atPosition) > atPosition + 1
               && !text.EndsWith(".");
    }
}
