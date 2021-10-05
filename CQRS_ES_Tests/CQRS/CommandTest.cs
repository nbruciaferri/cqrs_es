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
    public class CommandTest
    {
        private ICommand _command;
        private EventsRepository _eventsRepository;
        private EventsStore _eventsStore;
        private Product _product;
        private int _quantity;
        private Guid _aggregateId;

        [SetUp]
        public void Setup()
        {
            _aggregateId = Guid.NewGuid();
            _eventsRepository = new EventsRepository();
            _eventsStore = new EventsStore();
            _quantity = 10;
            _product = new Product("Prova", _aggregateId, _quantity, 100);
        }

        [Test]
        public void ConstructorTest()
        {
            _command = new AddCommand(_eventsStore, _eventsRepository, _product, _quantity);

            Assert.IsInstanceOf<ICommand>(_command);
            Assert.IsNotNull(((AddCommand)_command).EventsRepository);
            Assert.IsNotNull(((AddCommand)_command).Product);
            Assert.IsNotNull(((AddCommand)_command).Quantity);
        }

        [Test]
        public void ApplyTest()
        {
            _command = new AddCommand(_eventsStore, _eventsRepository, _product, _quantity);
            Assert.IsNotNull(_command);

            Assert.IsEmpty(((AddCommand)_command).EventsRepository.Events);

            _command.Apply();

            Assert.IsNotEmpty(((AddCommand)_command).EventsRepository.Events);
            Assert.AreEqual(((AddCommand)_command).EventsRepository.Events.Count, 1);
        }
    }
}
