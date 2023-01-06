using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Application.DTOs.AuthDTOs
{
    public class ValidatedUserCodeResultDTO
    {
        public User User { get; set; }
        public ValidationCode ValidationCode { get; set; }
    }
}
