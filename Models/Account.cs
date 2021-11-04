using System;
using System.Collections.Generic;

#nullable disable

namespace AccountMS.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int? CustomerId { get; set; }
        public string AccountType { get; set; }
        public double? Balance { get; set; }
    }
}
