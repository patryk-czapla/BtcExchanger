using System;
using System.ComponentModel.DataAnnotations;
namespace BtcExchanger.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        [Required]
        [Range( Single.Epsilon, Single.PositiveInfinity, 
        ErrorMessage = "Value for {0} must be > 0.")]
        public double btc_quantity { get; set; }
        [Required]
        [CreditCardAttribute]
        public string account_number { get; set; }
        [EmailAddressAttribute]
        public string email { get; set; }
        [PhoneAttribute]
        public string phone_number { get; set; }
        public string wallet { get; set; }
        
    }
}