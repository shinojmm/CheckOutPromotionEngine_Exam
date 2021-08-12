namespace CheckOut.Promotion.Engine.Entities
{
    public class OrderDetails
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public double FinalPrice { get; set; }
        public double DefaultPrice { get; set; }
        public bool HasOffer { get; set; }
        public bool IsValidated { get; set; }
    }
}
