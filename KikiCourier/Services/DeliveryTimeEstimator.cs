using KikiCourier.Interfaces;
using KikiCourier.Models;

namespace KikiCourier.Services
{
    public class DeliveryTimeEstimator : IDeliveryTimeEstimator
    {
        public void Estimate(List<Package> packages, int noOfVehicles, double maxSpeed, int maxCarriableWeight)
        {
            if (noOfVehicles <= 0 || maxSpeed <= 0) return;
            var heavyPackage = packages.FirstOrDefault(p => p.Weight > maxCarriableWeight);
            if (heavyPackage != null)
                throw new ValidationException($"ERROR: Package {heavyPackage.Id} ({heavyPackage.Weight}kg) exceeds max carriable weight ({maxCarriableWeight}kg)");
            var remaining = new List<Package>(packages);
            var vehicleQueue = new PriorityQueue<double, double>();

            for (int i = 0; i < noOfVehicles; i++)
                vehicleQueue.Enqueue(0.0, 0.0);

            while (remaining.Count > 0 && vehicleQueue.Count > 0)
            {
                double currentTime = vehicleQueue.Dequeue();

                var shipment = FindBestShipment(remaining, maxCarriableWeight);
                if (shipment.Count == 0) break;

                double maxDist = shipment.Max(p => p.Distance);
                double rawOneWay = maxDist / maxSpeed;
                double oneWayTime = Math.Floor(rawOneWay * 100) / 100;

                foreach (var pkg in shipment)
                {
                    double rawDel = currentTime + (pkg.Distance / maxSpeed);
                    pkg.EstimatedDeliveryTime = Math.Floor(rawDel * 100) / 100;
                }

                double returnTime = currentTime + 2 * oneWayTime;

                remaining.RemoveAll(p => shipment.Contains(p));
                vehicleQueue.Enqueue(returnTime, returnTime);
            }
        }

        private List<Package> FindBestShipment(List<Package> remaining, int maxWeight)
        {
            List<Package> best = new();
            (int size, double sumW, double negMaxD) bestScore = (0, 0, 0);

            int n = remaining.Count;
            for (int mask = 1; mask < (1 << n); mask++)
            {
                var subset = new List<Package>();
                int sumW = 0;
                double maxD = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        var p = remaining[i];
                        subset.Add(p);
                        sumW += p.Weight;
                        maxD = Math.Max(maxD, p.Distance);
                    }
                }

                if (sumW > maxWeight || subset.Count == 0) continue;

                var score = (subset.Count, (double)sumW, -maxD);
                if (CompareScore(score, bestScore) > 0)
                {
                    bestScore = score;
                    best = subset;
                }
            }
            return best;
        }

        private static int CompareScore((int count, double sumW, double negMaxD) a, (int count, double sumW, double negMaxD) b)
        {
            if (a.count != b.count)
                return a.count.CompareTo(b.count);
            if (a.sumW != b.sumW)
                return a.sumW.CompareTo(b.sumW);
            return a.negMaxD.CompareTo(b.negMaxD);
        }
    }
}
