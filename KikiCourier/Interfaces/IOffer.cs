using KikiCourier.Models;

namespace KikiCourier.Interfaces
{
    public interface IOffer
    {
        string Code { get; }
        double DiscountPercent { get; }
        bool IsApplicable(Package package);
    }
}
