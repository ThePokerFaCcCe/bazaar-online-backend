namespace BazaarOnline.Domain.Entities.Users
{
    public class ValidationCode
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Code { get; set; }

        public bool IsDeleted { get; set; } = false;

        public short TryCount { get; set; } = 0;

        public ActiveCodeType Type { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        public DateTime ExpireDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public bool IsExpired => (IsDeleted || (TryCount > 3) || (CreateDate >= ExpireDate));

        /// <summary>
        /// increase <see cref="TryCount"/> and set <see cref="IsDeleted"/> to true if max tries exceeded
        /// </summary>
        public void IncreaseTryCount()
        {
            TryCount++;

            if (IsExpired)
                Delete();
        }

        /// <summary>
        /// set <see cref="IsDeleted"/> to true and <see cref="DeleteDate"/> to DateTime.Now.
        /// Note: it only happens when model isn't deleted before.
        /// </summary>
        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeleteDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }

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
