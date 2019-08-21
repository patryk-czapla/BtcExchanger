using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BtcExchanger.Models
{
    public class Verification
    {
        public long VerificationId { get; set; }
        [Required]
        public long TransactionId { get; set; }
         [Required]
        public string verification_code { get; set; }
        public bool verified { get; set; }     
    }
}