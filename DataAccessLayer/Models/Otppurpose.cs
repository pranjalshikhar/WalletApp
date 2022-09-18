using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Otppurpose
    {
        public Otppurpose()
        {
            Otp = new HashSet<Otp>();
        }

        public byte OtppurposeId { get; set; }
        public string Otppurpose1 { get; set; }

        public virtual ICollection<Otp> Otp { get; set; }
    }
}
