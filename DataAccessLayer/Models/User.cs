using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            UserCard = new HashSet<UserCard>();
            UserTransaction = new HashSet<UserTransaction>();
        }

        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte StatusId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<UserCard> UserCard { get; set; }
        public virtual ICollection<UserTransaction> UserTransaction { get; set; }
    }
}
