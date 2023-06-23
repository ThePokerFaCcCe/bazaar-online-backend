using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaarOnline.Domain.Entities.Users;

namespace BazaarOnline.Domain.Entities.Conversations
{
    public class Blocklist
    {
        public Guid Id { get; set; }
        public string BlockerId { get; set; }
        public string BlockedUserId { get; set; }
        public DateTime CreateDate { get; set; }

        #region Relations

        public User Blocker { get; set; }
        public User BlockedUser { get; set; }

        #endregion
    }
}