using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class Offer003 : IOffer
    {
        public string Code => "OFR003";
        public double DiscountPercent => 5;

        public bool IsApplicable(Package package) =>
            package.Distance >= 50 && package.Distance <= 250 &&
            package.Weight >= 10 && package.Weight <= 150;
    }
}
