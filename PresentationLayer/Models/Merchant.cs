using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class Merchant
    {
        public short MerchantId { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
    }
}
