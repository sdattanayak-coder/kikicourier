using KikiCourier.Models;

namespace KikiCourier.Interfaces
{
    public interface IOfferService
    {
        double GetDiscountAmount(string offerCode, Package package);
    }
}
