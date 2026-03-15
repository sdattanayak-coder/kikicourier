using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IOfferService _offerService;

        public CostCalculator(IOfferService offerService) => _offerService = offerService;

        public void Calculate(IEnumerable<Package> packages, int baseDeliveryCost)
        {
            foreach (var package in packages)
            {
                package.DeliveryCost = baseDeliveryCost + (package.Weight * 10) + (package.Distance * 5);
                double rawDiscount = _offerService.GetDiscountAmount(package.OfferCode, package);
                package.Discount = Math.Round(rawDiscount, 0, MidpointRounding.AwayFromZero);
                package.TotalCost = Math.Round(package.DeliveryCost - package.Discount, 0, MidpointRounding.AwayFromZero);
            }
        }
    }
}
