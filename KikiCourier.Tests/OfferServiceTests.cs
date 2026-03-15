using KikiCourier.Interfaces;
using KikiCourier.Models;
using KikiCourier.Services;

namespace KikiCourier.Tests
{
    [TestFixture]
    public class OfferServiceTests
    {
        private IOfferService _service = new OfferService();

        [Test]
        public void OFR001_Applicable_Returns_10_Percent()
        {
            var pkg = new Package { Distance = 150, Weight = 100, DeliveryCost = 1000 };
            var discount = _service.GetDiscountAmount("OFR001", pkg);
            Assert.That(discount, Is.EqualTo(100));
        }

        [Test]
        public void OFR002_Applicable_Returns_7_Percent()
        {
            var pkg = new Package { Distance = 100, Weight = 150, DeliveryCost = 1000 };
            var discount = _service.GetDiscountAmount("OFR002", pkg);
            Assert.That(discount, Is.EqualTo(70));
        }

        [Test]
        public void InvalidOffer_Returns_Zero()
        {
            var pkg = new Package { Distance = 100, Weight = 100, DeliveryCost = 1000 };
            var discount = _service.GetDiscountAmount("INVALID", pkg);
            Assert.That(discount, Is.EqualTo(0));
        }

        [Test]
        public void NA_Returns_Zero()
        {
            var discount = _service.GetDiscountAmount("NA", new Package());
            Assert.That(discount, Is.EqualTo(0));
        }
    }
}
