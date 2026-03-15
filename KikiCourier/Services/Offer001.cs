using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class Offer001 : IOffer
    {
        public string Code => "OFR001";
        public double DiscountPercent => 10;

        public bool IsApplicable(Package package) =>
            package.Distance < 200 &&
            package.Weight >= 70 && package.Weight <= 200;
    }
}
