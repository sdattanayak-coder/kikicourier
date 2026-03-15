namespace KikiCourier.Models
{
    public class Package
    {
        public string Id { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Distance { get; set; }
        public string OfferCode { get; set; } = string.Empty;
        public double DeliveryCost { get; set; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double? EstimatedDeliveryTime { get; set; }
    }
}
