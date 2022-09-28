using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class MerchantTransaction
    {
        public long TransactionId { get; set; }
        public string EmailId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public byte PaymentTypeId { get; set; }
        public string Remarks { get; set; }
        public string Info { get; set; }
        public byte StatusId { get; set; }
    }
}
