namespace BazaarOnline.Domain.Entities.Users
{
    public class ValidationCode
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Code { get; set; }

        public ActiveCodeType Type { get; set; }
        
        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        #region Relations

        public User User { get; set; }
        
        #endregion
    }

    public enum ActiveCodeType
    {
        UserLogin,
        PhoneNumberActivation,
    }
}
