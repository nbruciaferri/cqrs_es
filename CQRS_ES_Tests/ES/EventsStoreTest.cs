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
    public class EventsStoreTest
    {
        private EventsStore _eventsStore;
        private EventsRepository _eventsRepository;
        private Guid _aggregateId;

        [SetUp]
        public void Setup()
        {
            _eventsStore = new EventsStore();
            _eventsStore.ClearEventsRepository();

            Assert.IsEmpty(_eventsStore.GetEventsRepository());

            _eventsRepository = new EventsRepository();
            PopulateRepo();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _eventsStore.ClearEventsRepository();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsInstanceOf<IEventStore>(_eventsStore);
            Assert.IsNotEmpty(_eventsStore.GetEventsRepository());
        }


        [Test]
        public void SaveEventsTest()
        {
            Assert.IsNotEmpty(_eventsStore.GetEventsRepository());

            var e = _eventsStore.GetEventsRepository().First().Value;

            Assert.IsInstanceOf<List<IEvent>>(e);
            Assert.IsNotEmpty(e);
            Assert.AreEqual(e.Count, 1);
            Assert.IsInstanceOf<ProductsAddedEvent>(e.First());
            Assert.AreEqual(((ProductsAddedEvent)e.First()).Quantity, 1);
            Assert.AreEqual(((ProductsAddedEvent)e.First()).EventNumber, 1);
            Assert.AreEqual(((ProductsAddedEvent)e.First()).AggregateId, _aggregateId);
        }

        [Test]
        public void GetLastEventNUmberTest()
        {
            Assert.AreEqual(_eventsStore.GetLastEventNumber(_aggregateId), 1);

            AddEventToAlreadyExistingRepo(_aggregateId);
            Assert.AreEqual(_eventsStore.GetLastEventNumber(_aggregateId), 2);
        }

        [Test]
        public void GetSavedAggregateIdsTest()
        {
            Assert.AreEqual(_eventsStore.GetSavedAggregateIds(), new List<Guid> { _aggregateId });

            AddEventToAlreadyExistingRepo(_aggregateId);
            Assert.AreEqual(_eventsStore.GetSavedAggregateIds(), new List<Guid> { _aggregateId });

            Guid newAggregateId = Guid.NewGuid();
            AddEventToAlreadyExistingRepo(newAggregateId);
            Assert.AreEqual(_eventsStore.GetSavedAggregateIds().Count, 2);
            Assert.AreEqual(_eventsStore.GetSavedAggregateIds(), new List<Guid> { _aggregateId, newAggregateId });
        }

        [Test]
        public void GetEventsByAggregateTest()
        {
            Assert.AreEqual(_eventsStore.GetEventsByAggregate(_aggregateId).Count, 1);

            AddEventToAlreadyExistingRepo(_aggregateId);
            Assert.AreEqual(_eventsStore.GetEventsByAggregate(_aggregateId).Count, 2);
        }

        public void PopulateRepo()
        {
            _aggregateId = Guid.NewGuid();

            Dictionary<Guid, List<IEvent>> events = new Dictionary<Guid, List<IEvent>>
            {
                { _aggregateId, new List<IEvent> { new ProductsAddedEvent(_eventsRepository, new Product("prova", _aggregateId, 1, 10), 1) } }
            };

            _eventsStore.SaveEvents(_aggregateId, events, 0);
        }

        public void AddEventToAlreadyExistingRepo(Guid aggregateId)
        {
            Dictionary<Guid, List<IEvent>> events = new Dictionary<Guid, List<IEvent>>
            {
                {aggregateId, new List<IEvent> { new ProductsAddedEvent(_eventsRepository, new Product("prova", aggregateId, 5, 10), 2) } }
            };

            _eventsStore.SaveEvents(aggregateId, events, _eventsStore.GetLastEventNumber(aggregateId));
        }
    }
}
