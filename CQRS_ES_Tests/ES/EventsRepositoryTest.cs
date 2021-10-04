using CQRS_ES.Domain;
using CQRS_ES.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_ES_Tests.ES
{
    [TestFixture]
    public class EventsRepositoryTest
    {
        private EventsRepository _eventsRepository;
        private Guid _aggregateId;


        [SetUp]
        public void Setup()
        {
            _eventsRepository = new EventsRepository();
            _aggregateId = Guid.NewGuid();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_eventsRepository.Events);
            Assert.IsEmpty(_eventsRepository.Events);
        }

        [Test]
        public void GetUncommittedEventsTest()
        {
            Assert.IsNotNull(_eventsRepository.GetUncommittedEvents());
            Assert.IsEmpty(_eventsRepository.GetUncommittedEvents());

            _eventsRepository.Events.Add(_aggregateId, new List<IEvent> { new ProductsAddedEvent(_eventsRepository, new Product("prova", _aggregateId, 1, 10), 1) });
            Assert.IsNotEmpty(_eventsRepository.GetUncommittedEvents());
            Assert.AreEqual(_eventsRepository.GetUncommittedEvents().Count, 1);
            Assert.AreEqual(_eventsRepository.GetUncommittedEvents(), new List<Guid> { _aggregateId });
        }
    }
}
