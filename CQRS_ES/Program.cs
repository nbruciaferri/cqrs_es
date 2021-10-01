using System;
using CQRS_ES.CQRS;
using CQRS_ES.Domain;
using CQRS_ES.ES;

namespace CQRS_ES
{
    class Program
    {
        public static int eventNumber;

        static void Main(string[] args)
        {
            eventNumber = -1;

            // InMemory Cache
            EventsStore eventsStore = new EventsStore();
            EventsRepository eventsRepository = new EventsRepository();

            Product p = new Product("Macbook", Guid.NewGuid(), 5, 2700);

            var command = new AddCommand(eventsRepository, p, p.Quantity);
            command.Apply();
            eventNumber++;

            if (eventsRepository.UncommittedEvents)
                eventsStore.SaveEvents(p.AggregateId, eventsRepository.Events, eventNumber);

            QueryModel queryModel = new QueryModel(eventsStore, p.AggregateId);
            queryModel.ShowProduct();

            Product p1 = new Product("Macbook", p.AggregateId, 10, 2700);
            var command1 = new AddCommand(eventsRepository, p1, p1.Quantity);
            command1.Apply();
            eventNumber++;

            if (eventsRepository.UncommittedEvents)
                eventsStore.SaveEvents(p1.AggregateId, eventsRepository.Events, eventNumber);

            QueryModel queryModel1 = new QueryModel(eventsStore, p1.AggregateId);
            queryModel1.ShowProduct();
        }
    }
}
