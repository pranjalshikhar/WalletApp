using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
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
    }
}
