using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            MerchantTransaction = new HashSet<MerchantTransaction>();
            UserTransaction = new HashSet<UserTransaction>();
        }

        public byte PaymentTypeId { get; set; }
        public string PaymentFrom { get; set; }
        public string PaymentTo { get; set; }
        public bool PaymentType1 { get; set; }

        public virtual ICollection<MerchantTransaction> MerchantTransaction { get; set; }
        public virtual ICollection<UserTransaction> UserTransaction { get; set; }
    }
}
