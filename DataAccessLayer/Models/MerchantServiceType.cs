using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class MerchantServiceType
    {
        public MerchantServiceType()
        {
            MerchantServiceMapping = new HashSet<MerchantServiceMapping>();
        }

        public byte ServiceId { get; set; }
        public string ServiceType { get; set; }

        public virtual ICollection<MerchantServiceMapping> MerchantServiceMapping { get; set; }
    }
}
