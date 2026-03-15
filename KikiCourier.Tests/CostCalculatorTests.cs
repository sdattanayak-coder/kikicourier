using KikiCourier.Models;
using KikiCourier.Services; 

namespace KikiCourier.Tests
{
    [TestFixture]
    public class CostCalculatorTests
    {
        [Test]
        public void Sample_Problem_01_Matches_PDF_Exactly()
        {
            var packages = new List<Package>
        {
            new() { Id = "PKG1", Weight = 5, Distance = 5, OfferCode = "OFR001" },
            new() { Id = "PKG2", Weight = 15, Distance = 5, OfferCode = "OFR002" },
            new() { Id = "PKG3", Weight = 10, Distance = 100, OfferCode = "OFR003" }
        };

            var calc = new CostCalculator(new OfferService());
            calc.Calculate(packages, 100);

            Assert.That(packages[0].Discount, Is.EqualTo(0));
            Assert.That(packages[0].TotalCost, Is.EqualTo(175));

            Assert.That(packages[2].Discount, Is.EqualTo(35));
            Assert.That(packages[2].TotalCost, Is.EqualTo(665));
        }
    }
}
