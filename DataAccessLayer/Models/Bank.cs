using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Bank
    {
        public Bank()
        {
            UserCard = new HashSet<UserCard>();
        }

        public byte BankId { get; set; }
        public string BankName { get; set; }

        public virtual ICollection<UserCard> UserCard { get; set; }
    }
}
