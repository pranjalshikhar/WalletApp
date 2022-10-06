using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class UserTransaction
    {
        public long UserTransactionId { get; set; }
        public string EmailId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public byte PaymentTypeId { get; set; }
        public string Remarks { get; set; }
        public string Info { get; set; }
        public byte StatusId { get; set; }
        public short? PointsEarned { get; set; }
        public bool IsRedeemed { get; set; }
    }
}
