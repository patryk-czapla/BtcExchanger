using System;
using System.ComponentModel.DataAnnotations;
namespace BtcExchanger.Models
{
    public class VerificationItem
    {
        public long Id { get; set; }
        public long order_Id { get; set; }
        public string verification_code { get; set; }
        public bool verified { get; set; }     
    }
}