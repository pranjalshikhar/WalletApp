using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class PaymentType
    {
        public byte PaymentTypeId { get; set; }
        public string PaymentFrom { get; set; }
        public string PaymentTo { get; set; }
        public bool PaymentType1 { get; set; }
    }
}
