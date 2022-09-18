using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Otp
    {
        public int Otpid { get; set; }
        public string Otpvalue { get; set; }
        public string ReferenceId { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public byte OtppurposeId { get; set; }
        public bool? IsValid { get; set; }

        public virtual Otppurpose Otppurpose { get; set; }
    }
}
