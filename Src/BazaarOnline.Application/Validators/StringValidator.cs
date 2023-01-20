using System.Text.RegularExpressions;

namespace BazaarOnline.Application.Validators;

public class StringValidator
{
    public const string EmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                       + "@"
                                       + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

    public const string PhonePattern = @"^[09\d]{11}$";

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, EmailPattern, RegexOptions.Singleline);
    }

    public static bool IsValidPhone(string phone)
    {
        return Regex.IsMatch(phone, PhonePattern, RegexOptions.Singleline);
    }
}