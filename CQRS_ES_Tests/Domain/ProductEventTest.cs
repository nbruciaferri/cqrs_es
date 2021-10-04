using CQRS_ES.Domain;
using CQRS_ES.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_ES_Tests.Domain
{
    [TestFixture]
    public class ProductEventTest
    {
        private Guid _aggregateId;
        private Event _event;
        private EventsRepository _eventsRepository;
        private Product _product;
        private int _quantity;

        [SetUp]
        public void Setup()
        {
            _aggregateId = Guid.NewGuid();
            _eventsRepository = new EventsRepository();
            _quantity = 10;
            _product = new Product("Prova", _aggregateId, _quantity, 100);
        }

        [Test]
        public void ConstructorTest()
        {
            _event = new ProductsAddedEvent(_eventsRepository, _product, _quantity);

            Assert.IsInstanceOf<IEvent>(_event);
            Assert.IsInstanceOf<Event>(_event);
            Assert.IsInstanceOf<ProductsAddedEvent>(_event);

            Assert.AreEqual(_event.AggregateId, _aggregateId);
            Assert.AreEqual(_event.Quantity, _quantity);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(7)]
        public void SetEventNumberTest(int eventNumber)
        {
            _event = new ProductsAddedEvent(_eventsRepository, _product, _quantity);

            _event.SetEventNumber(eventNumber);
            Assert.AreEqual(_event.EventNumber, eventNumber);
        }

        [Test]
        public void SubscribeEventTest()
        {
            Assert.IsEmpty(_eventsRepository.Events);

            _event = new ProductsAddedEvent(_eventsRepository, _product, _quantity);
            _event.Subscribe();

            Assert.IsNotEmpty(_eventsRepository.Events);
        }

    }
}
