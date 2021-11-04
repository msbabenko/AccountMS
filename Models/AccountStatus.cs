using System;
using System.Collections.Generic;

#nullable disable

namespace AccountMS.Models
{
    public partial class AccountStatus
    {
        public int StatusId { get; set; }
        public int? AccountId { get; set; }
        public string AccountCreationStatus { get; set; }
    }
}
