using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Conversation
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int AdvertisementId { get; set; }

        public string OwnerId { get; set; }

        public string CustomerId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);


        #region Relations

        public Advertisement Advertisement { get; set; }
        public User Customer { get; set; }
        public User Owner { get; set; }

        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<DeletedConversation> DeletedConversations { get; set; }

        #endregion
    }
}