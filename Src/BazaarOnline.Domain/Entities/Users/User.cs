using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BazaarOnline.Domain.Entities.Advertisements;

namespace BazaarOnline.Domain.Entities.Users
{
    public class User
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public bool IsPhoneNumberActive { get; set; }

        #region Relations

        public IEnumerable<ValidationCode> ValidationCodes { get; set; }

        public IEnumerable<Advertisement> Advertisements { get; set; }

        #endregion
    }
}