using BazaarOnline.Application.DTOs.UserDTOs;
using BazaarOnline.Application.Interfaces.Users;
using BazaarOnline.Application.Utils.Extensions;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Domain.Interfaces;

namespace BazaarOnline.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public User CreateUserByEmail(CreateUserByEmailDTO dto)
        {
            dto.TrimStrings();
            dto.Email = dto.Email.ToLower();

            var user = new User();
            user.FillFromObject(dto);

            _repository.Add(user);
            _repository.Save();

            return user;
        }

        public User CreateUserByPhoneNumber(CreateUserByPhoneNumberDTO dto)
        {
            dto.TrimStrings();

            var user = new User();
            user.FillFromObject(dto);

            _repository.Add(user);
            _repository.Save();

            return user;
        }

        public User? FindUserByEmail(string email)
        {
            return _repository.GetAll<User>()
                .SingleOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public User? FindUserByPhoneNumber(string phoneNumber)
        {
            return _repository.GetAll<User>()
                .SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public bool IsUserExistsByEmail(string email)
        {
            return _repository.GetAll<User>()
                .Any(u => u.Email == email);
        }

        public void ActivateUser(User user)
        {
            if (!user.IsActive)
            {
                user.IsActive = true;
                user.IsPhoneNumberActive = true;
                _repository.Update(user);
                _repository.Save();
            }
        }

        public UpdateUserLastSeenResultDTO UpdateUserLastSeenToNow(string userId)
        {
            var user = _repository.Get<User>(userId);
            user.LastSeen = DateTime.Now;
            _repository.Update(user);
            _repository.Save();

            return new UpdateUserLastSeenResultDTO
            {
                UserId = userId,
                LastSeen = user.LastSeen,
            };
        }
    }
}