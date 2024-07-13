using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Domain.Entities.Users
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; }

        public bool IsPhoneNumberActive { get; set; }

        #region Chat

        public DateTime LastSeen { get; set; }

        public string DisplayName { get; set; }

        public int AnswerHourStart { get; set; }

        public int AnswerHourEnd { get; set; }

        #endregion

        #region Relations

        public IEnumerable<ValidationCode> ValidationCodes { get; set; }

        public IEnumerable<Advertisement> Advertisements { get; set; }

        public IEnumerable<UserAdvertisementHistory> WatchedAdvertisements { get; set; }

        public IEnumerable<UserAdvertisementNote> AdvertisementNotes { get; set; }

        public IEnumerable<UserAdvertisementBookmark> AdvertisementBookmarks { get; set; }

        #endregion
    }
}