using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Conversation
    {
        public int Id { get; set; }

        public int AdvertisementId { get; set; }

        public int OwnerId { get; set; }

        public int SenderId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }


        #region Relations

        public Advertisement Advertisement { get; set; }
        public User Sender { get; set; }
        public User Owner { get; set; }

        #endregion


    }
}
