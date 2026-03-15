using KikiCourier.Models;

namespace KikiCourier.Interfaces
{
    public interface ICostCalculator
    {
        void Calculate(IEnumerable<Package> packages, int baseDeliveryCost);
    }
}
