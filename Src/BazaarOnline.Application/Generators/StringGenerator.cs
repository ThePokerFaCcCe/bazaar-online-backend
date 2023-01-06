using System.Security.Cryptography;

namespace BazaarOnline.Application.Generators
{
    public class StringGenerator
    {
        public static string GenerateActiveCode()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }
        public static string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString();
        }
        public static string GenerateUniqueCodeWithoutDash()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GenerateShortKey()
        {
            return Guid.NewGuid().ToString().Substring(5);
        }
    }
}
