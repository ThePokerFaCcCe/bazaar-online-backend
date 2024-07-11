using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Domain.Entities.Users;

public class UserAdvertisementHistory
{
    public int Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    public bool IsDeleted { get; set; } = false;

    public string UserId { get; set; }

    public int AdvertisementId { get; set; }

    #region Relations

    public User User { get; set; }
    public Advertisement Advertisement { get; set; }

    #endregion
}