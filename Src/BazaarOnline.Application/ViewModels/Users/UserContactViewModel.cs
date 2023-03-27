namespace BazaarOnline.Application.ViewModels.Users;

public class UserContactViewModel
{
    public bool Success { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}