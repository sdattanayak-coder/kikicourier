using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class OfferService : IOfferService
    {
        private readonly List<IOffer> _offers = new()
    {
        new Offer001(),
        new Offer002(),
        new Offer003()
    };

        public double GetDiscountAmount(string offerCode, Package package)
        {
            if (string.IsNullOrEmpty(offerCode) || offerCode == "NA") return 0;

            var offer = _offers.FirstOrDefault(o =>
                o.Code.Equals(offerCode, StringComparison.OrdinalIgnoreCase));

            if (offer == null || !offer.IsApplicable(package))
                return 0;
            return Math.Round((offer.DiscountPercent / 100.0) * package.DeliveryCost, 0, MidpointRounding.AwayFromZero);
        }
    }
}
