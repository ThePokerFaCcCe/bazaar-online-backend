namespace BazaarOnline.Application.ViewModels.Users.UserDashboardViewModels
{
    public class UserShortDashboardDetailViewModel
    {
        public string Id { get; set; } = string.Empty;

        public UserShortDashboardDataDetailViewModel Data { get; set; }
    }

    public class UserShortDashboardDataDetailViewModel
    {
        public string DisplayName { get; set; } = string.Empty;

        public int AnswerHourStart { get; set; } = 0;

        public int AnswerHourEnd { get; set; } = 23;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = null;
    }
}
