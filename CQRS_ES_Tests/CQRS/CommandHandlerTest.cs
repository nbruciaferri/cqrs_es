using CQRS_ES.CQRS;
using CQRS_ES.Domain;
using CQRS_ES.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_ES_Tests.CQRS
{
    [TestFixture]
    public class CommandHandlerTest
    {
        private EventsStore _eventsStore;
        private EventsRepository _eventsRepository;
        private Product _product;
        private CommandHandler _commandHandler;
        private ICommand _command;
        private Guid _aggregateId;
        private Dictionary<Guid, int> _dAggregateId;

        [SetUp]
        public void Setup()
        {
            _eventsStore = new EventsStore();
            _eventsRepository = new EventsRepository();
            _commandHandler = new CommandHandler(_eventsStore);

            _aggregateId = Guid.NewGuid();
            _product = new Product("Prova", _aggregateId, 10, 100);

            _dAggregateId = new Dictionary<Guid, int>();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_commandHandler);
            Assert.IsNotNull(_eventsStore);
        }

        [Test]
        public void HandleCommandTest()
        {
            //Assert.AreEqual(_dAggregateId[_aggregateId], 0);

            _command = new AddCommand(_eventsRepository, _product, 15);
            int retValue = _commandHandler.HandleCommand(_command, _dAggregateId);

            Assert.AreEqual(retValue, 1);
            Assert.AreEqual(_dAggregateId[_aggregateId], 0);
        }
    }
}
