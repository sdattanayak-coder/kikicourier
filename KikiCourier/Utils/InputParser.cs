using KikiCourier.Models;

namespace KikiCourier.Utils
{
    public static class InputParser
    {
        public static (int baseCost, List<Package> packages, int? noVehicles, double? speed, int? maxW)
            Parse(string[] lines)
        {
            if (lines.Length == 0)
                throw new ValidationException("ERROR: Input is empty. Please provide at least the first line.");

            // First line: base_delivery_cost no_of_packages
            var firstParts = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (firstParts.Length != 2)
                throw new ValidationException("ERROR: First line must be exactly: base_delivery_cost no_of_packages");

            if (!int.TryParse(firstParts[0], out int baseCost) || baseCost < 0)
                throw new ValidationException("ERROR: Base delivery cost must be a valid non-negative integer");

            if (!int.TryParse(firstParts[1], out int noOfPackages) || noOfPackages < 0)
                throw new ValidationException("ERROR: Number of packages must be a valid non-negative integer");

            if (lines.Length < 1 + noOfPackages)
                throw new ValidationException($"ERROR: Expected {noOfPackages} package lines, but only found {lines.Length - 1}");

            var packages = new List<Package>();
            for (int i = 0; i < noOfPackages; i++)
            {
                var line = lines[1 + i];
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 3)
                    throw new ValidationException($"ERROR: Package line {i + 1} must have at least: pkg_id weight distance [offer_code]");

                if (!int.TryParse(parts[1], out int weight) || weight <= 0)
                    throw new ValidationException($"ERROR: Package {parts[0]} - Weight must be positive integer");

                if (!int.TryParse(parts[2], out int distance) || distance < 0)
                    throw new ValidationException($"ERROR: Package {parts[0]} - Distance must be non-negative integer");

                string offerCode = parts.Length > 3 ? parts[3] : "NA";

                packages.Add(new Package
                {
                    Id = parts[0],
                    Weight = weight,
                    Distance = distance,
                    OfferCode = offerCode
                });
            }

            // Optional vehicle line
            int? noVehicles = null;
            double? speed = null;
            int? maxW = null;

            if (lines.Length > 1 + noOfPackages)
            {
                var vehicleLine = lines[1 + noOfPackages];
                var vParts = vehicleLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (vParts.Length != 3)
                    throw new ValidationException("ERROR: Vehicle line must be exactly: no_of_vehicles max_speed max_carriable_weight");

                if (!int.TryParse(vParts[0], out int vehicles) || vehicles < 1)
                    throw new ValidationException("ERROR: Number of vehicles must be at least 1");

                if (!double.TryParse(vParts[1], out double maxSpeed) || maxSpeed <= 0)
                    throw new ValidationException("ERROR: Max speed must be greater than 0");

                if (!int.TryParse(vParts[2], out int maxWeight) || maxWeight <= 0)
                    throw new ValidationException("ERROR: Max carriable weight must be greater than 0");

                noVehicles = vehicles;
                speed = maxSpeed;
                maxW = maxWeight;
            }

            return (baseCost, packages, noVehicles, speed, maxW);
        }
    }
}
