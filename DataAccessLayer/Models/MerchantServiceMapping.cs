using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class MerchantServiceMapping
    {
        public short MerchantServiceMappingId { get; set; }
        public string EmailId { get; set; }
        public byte ServiceId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }

        public virtual Merchant Email { get; set; }
        public virtual MerchantServiceType Service { get; set; }
    }
}
