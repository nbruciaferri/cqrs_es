using CQRS_ES.CQRS;
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
    public class QueryModelTest
    {
        private EventsStore _eventsStore;
        private Guid _aggregateId;
        private QueryModel _queryModel;

        [SetUp]
        public void Setup()
        {
            _eventsStore = new EventsStore();
            _aggregateId = Guid.NewGuid();

            _queryModel = new QueryModel(_eventsStore, _aggregateId);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(_queryModel);
        }

        //[Test]
        //public void ShowProductTest()
        //{
        //    Assert.IsEmpty(_eventsStore.GetEventsRepository());

        //    _queryModel.ShowProduct();
        //}
    }
}
