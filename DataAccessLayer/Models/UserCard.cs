using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class UserCard
    {
        public int UserCardMappingId { get; set; }
        public string EmailId { get; set; }
        public string CardNumber { get; set; }
        public byte BankId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public byte StatusId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual User Email { get; set; }
    }
}
