using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;

namespace BazaarOnline.Application.Services.Users
{
    public class ValidationCodeService : IValidationCodeService
    {
        private readonly IRepository _repository;

        public ValidationCodeService(IRepository repository)
        {
            _repository = repository;
        }

        public bool IsActiveEmailValidationExists(string userId)
        {
            return _repository.GetAll<ValidationCode>()
                .Any(v => v.UserId == userId
                          && v.Type == ActiveCodeType.UserLogin
                          && !v.IsDeleted
                          && !((v.TryCount > 3) || (v.CreateDate >= v.ExpireDate)));
        }
        public bool IsActivePhoneNumberValidationExists(string userId)
        {
            return _repository.GetAll<ValidationCode>()
                .Any(v => v.UserId == userId
                          && v.Type == ActiveCodeType.UserLogin
                          && !v.IsDeleted
                          && !((v.TryCount > 3) || (v.CreateDate >= v.ExpireDate)));
        }

        public void DeleteValidationCode(ValidationCode validationCode)
        {
            _repository.Remove(validationCode);
            _repository.Save();
        }
    }
}
