using KikiCourier.Interfaces;
using KikiCourier.Services;
using KikiCourier.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace KikiCourier;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IOfferService, OfferService>();
                    services.AddSingleton<ICostCalculator, CostCalculator>();
                    services.AddSingleton<IDeliveryTimeEstimator, DeliveryTimeEstimator>();
                })
                .Build();

            var costCalc = host.Services.GetRequiredService<ICostCalculator>();
            var timeEst = host.Services.GetRequiredService<IDeliveryTimeEstimator>();

            Console.WriteLine("=== KikiCourier Started ===");
            Console.WriteLine("Paste your input below.");
            Console.WriteLine("After the last line, press ENTER twice (blank line) to finish.\n");

            var inputLines = new List<string>();
            string? line;
            while ((line = Console.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrEmpty(line)) break;
                inputLines.Add(line);
            }

            if (inputLines.Count == 0)
                throw new ValidationException("ERROR: No input provided.");

            var (baseCost, packages, noVehicles, speed, maxW) = InputParser.Parse(inputLines.ToArray());

            costCalc.Calculate(packages, baseCost);

            if (noVehicles.HasValue && speed.HasValue && maxW.HasValue)
                timeEst.Estimate(packages, noVehicles.Value, speed.Value, maxW.Value);
            Console.WriteLine("\nDone! Below is the output...\n");
            foreach (var p in packages)
            {
                string output = $"{p.Id} {p.Discount:0} {p.TotalCost:0}";
                if (p.EstimatedDeliveryTime.HasValue)
                    output += $" {p.EstimatedDeliveryTime}";
                Console.WriteLine(output);
            }

            Console.WriteLine("\nDone! Press any key to close...");
            Console.ReadKey();
        }
        catch (ValidationException ex)
        {
            Console.Error.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"UNEXPECTED ERROR: {ex.Message}");
            Environment.Exit(1);
        }
    }
}