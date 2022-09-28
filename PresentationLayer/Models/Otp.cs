using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class Otp
    {
        public int Otpid { get; set; }
        public string Otpvalue { get; set; }
        public string ReferenceId { get; set; }
        public DateTime ExpiryDateTime { get; set; }
        public byte OtppurposeId { get; set; }
        public bool? IsValid { get; set; }
    }
}
