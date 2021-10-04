using CQRS_ES.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_ES_Tests.Domain
{
    [TestFixture]
    public class ProductTest
    {
        private Product _product;
        private Guid _aggregateId;

        [SetUp]
        public void Setup()
        {
            _aggregateId = Guid.NewGuid();
        }

        [Test]
        public void ConstructorTest()
        {
            _product = new Product("Prodotto prova", _aggregateId, 5, 100);

            Assert.IsInstanceOf<IProduct>(_product);
            Assert.AreEqual(_product.Name, "Prodotto prova");
            Assert.AreEqual(_product.AggregateId, _aggregateId);
            Assert.AreEqual(_product.ProductId, $"Prodotto prova@{_aggregateId}");
            Assert.AreEqual(_product.Quantity, 5);
        }

        [Test]
        public void GetAvailabilityTest()
        {
            _product = new Product("Prodotto prova", _aggregateId, 5, 100);
            Assert.IsTrue(_product.GetAvailability());

            _product = new Product("Prodotto prova", _aggregateId, 0, 100);
            Assert.IsFalse(_product.GetAvailability());
        }

        [Test]
        public void GetPriceTest()
        {
            _product = new Product("Prodotto prova", _aggregateId, 5, 100);
            Assert.AreEqual(_product.GetPrice(), 100);

            _product = new Product("Prodotto prova", _aggregateId, 2, 500);
            Assert.AreNotEqual(_product.GetPrice(), 100);
        }
    }
}
