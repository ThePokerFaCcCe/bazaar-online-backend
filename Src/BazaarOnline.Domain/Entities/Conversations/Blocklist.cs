using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Blocklist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BlockerId { get; set; }
        public string BlockedUserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        #region Relations

        public User Blocker { get; set; }
        public User BlockedUser { get; set; }

        #endregion
    }
}