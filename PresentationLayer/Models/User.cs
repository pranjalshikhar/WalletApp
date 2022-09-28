using System;
using System.Collections.Generic;

namespace PresentationLayer.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public byte StatusId { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
    }
}
