using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Application.Interfaces.Users
{
    public interface IValidationCodeService
    {
        bool IsActiveEmailValidationExists(string userId);
        bool IsActivePhoneNumberValidationExists(string userId);

        void DeleteValidationCode(ValidationCode validationCode);
    }
}
