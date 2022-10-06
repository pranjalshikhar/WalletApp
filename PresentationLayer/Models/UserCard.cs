using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
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
    }
}
