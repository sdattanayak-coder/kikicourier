using KikiCourier.Models;

namespace KikiCourier.Interfaces
{
    public interface IDeliveryTimeEstimator
    {
        void Estimate(List<Package> packages, int noOfVehicles, double maxSpeed, int maxCarriableWeight);
    }
}
