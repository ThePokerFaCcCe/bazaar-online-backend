using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Entities.Maps;
using BazaarOnline.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace BazaarOnline.Domain.Entities.Advertisements;

public class Advertisement
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public bool ShowExactCoordinates { get; set; }

    public bool HasCoordinates => Longitude != null && Latitude != null;

    public AdvertisementStatusTypeEnum StatusType { get; set; }

    public string? StatusReason { get; set; }

    public AdvertisementContactTypeEnum ContactType { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    public DateTime UpdateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

    public int CategoryId { get; set; }

    public int ProvinceId { get; set; }

    public int CityId { get; set; }

    public string UserId { get; set; }

    [NotMapped]
    public bool IsDeleted =>
        StatusType is AdvertisementStatusTypeEnum.DeletedByAdmin or AdvertisementStatusTypeEnum.DeletedByUser;

    #region Relations

    public IEnumerable<AdvertisementFeature> AdvertisementFeatures { get; set; }

    public IEnumerable<AdvertisementPicture> Pictures { get; set; }

    public IEnumerable<UserAdvertisementHistory> WatchedUsers { get; set; }

    public IEnumerable<UserAdvertisementNote> UserNotes { get; set; }

    public IEnumerable<UserAdvertisementBookmark> UserBookmarks { get; set; }

    public Category Category { get; set; }

    public Province Province { get; set; }

    public City City { get; set; }

    public User User { get; set; }

    #endregion
}