namespace BazaarOnline.Application.ViewModels.Users;

public class UserContactViewModel
{
    public bool Success { get; set; } = true;

    public string ErrorMessage { get; set; } = string.Empty;

    public int AnswerHourStart { get; set; } = 0;

    public int AnswerHourEnd { get; set; } = 23;

    public string PhoneNumber { get; set; } = null;

    public string Email { get; set; } = string.Empty;
}