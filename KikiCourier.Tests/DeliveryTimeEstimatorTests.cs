using KikiCourier.Models;
using KikiCourier.Services;

namespace KikiCourier.Tests
{
    [TestFixture]
    public class DeliveryTimeEstimatorTests
    {
        [Test]
        public void Sample_Problem_02_Matches_PDF_Exactly()
        {
            var packages = new List<Package>
        {
            new() { Id = "PKG1", Weight = 50, Distance = 30, OfferCode = "OFR001" },
            new() { Id = "PKG2", Weight = 75, Distance = 125, OfferCode = "OFR008" },
            new() { Id = "PKG3", Weight = 175, Distance = 100, OfferCode = "OFR003" },
            new() { Id = "PKG4", Weight = 110, Distance = 60, OfferCode = "OFR002" },
            new() { Id = "PKG5", Weight = 155, Distance = 95, OfferCode = "NA" }
        };

            var estimator = new DeliveryTimeEstimator();
            estimator.Estimate(packages, 2, 70, 200);

            Assert.That(packages[0].EstimatedDeliveryTime, Is.EqualTo(3.98));
            Assert.That(packages[1].EstimatedDeliveryTime, Is.EqualTo(1.78));
            Assert.That(packages[2].EstimatedDeliveryTime, Is.EqualTo(1.42));
            Assert.That(packages[3].EstimatedDeliveryTime, Is.EqualTo(0.85));
            Assert.That(packages[4].EstimatedDeliveryTime, Is.EqualTo(4.19));
        }
    }
}
