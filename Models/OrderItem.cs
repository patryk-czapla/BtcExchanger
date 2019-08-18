namespace dotnet_bitcoin.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        public double btc_quantity { get; set; }
        public string account_number { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
    }
}