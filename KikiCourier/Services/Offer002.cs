using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class Offer002 : IOffer
    {
        public string Code => "OFR002";
        public double DiscountPercent => 7;

        public bool IsApplicable(Package package) =>
            package.Distance >= 50 && package.Distance <= 150 &&
            package.Weight >= 100 && package.Weight <= 250;
    }
}
