using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BtcExchanger.Models
{
    public class Transaction
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
        [JsonConverter(typeof(StringEnumConverter))]
        public Status? status { get; set; }
        
    }
    public enum Status
    {
        [EnumMember(Value = "Waiting for verification.")]
        WAITING_FOR_VERIFICATION,
        [EnumMember(Value = "Waiting for money transfer.")]
        WAITING_FOR_TRANSFER,
        [EnumMember(Value = "Transaction ended.")]
        ENDED,
    }
}