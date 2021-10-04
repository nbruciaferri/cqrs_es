using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CQRS_ES.CQRS;
using CQRS_ES.Domain;
using CQRS_ES.ES;

namespace CQRS_ES
{
    class Program
    {
        static void Main()
        {
            // InMemory Cache
            Dictionary<Guid, int> dAggregateId = new Dictionary<Guid, int>();
            EventsStore eventsStore = new EventsStore();
            EventsRepository eventsRepository = new EventsRepository();
            CommandHandler commandHandler = new CommandHandler(eventsStore);


            Product p = new Product("Macbook", Guid.NewGuid(), 5, 2700);
            commandHandler.HandleCommand(new AddCommand(eventsRepository, p, p.Quantity), dAggregateId);
            
            // 2 Seconds
            Thread.Sleep(2000);

            Product p1 = new Product("Macbook", p.AggregateId, 10, 2700);
            commandHandler.HandleCommand(new AddCommand(eventsRepository, p1, p1.Quantity), dAggregateId);
            
            // 3 Seconds
            Thread.Sleep(3000);

            Product p2 = new Product("Gaming Pc", Guid.NewGuid(), 27, 1500);
            commandHandler.HandleCommand(new AddCommand(eventsRepository, p2, p2.Quantity), dAggregateId);
            
            var uncommittedEvents = eventsRepository.GetUncommittedEvents();
            if (uncommittedEvents.Count > 0)
            {
                foreach (var id in uncommittedEvents)
                {
                    Guid aggregateId = id;
                    int lastSavedEventNumber = eventsStore.GetLastEventNumber(aggregateId);

                    eventsStore.SaveEvents(aggregateId, eventsRepository.Events, lastSavedEventNumber);
                }
            }

            foreach (var id in dAggregateId.Keys)
            {
                QueryModel model = new QueryModel(eventsStore, id);
                model.ShowProduct();

            }

            // 2 Seconds
            Thread.Sleep(2000);

            Product p3 = new Product("Gaming Pc", p2.AggregateId, -12, 1500);
            commandHandler.HandleCommand(new AddCommand(eventsRepository, p3, p3.Quantity), dAggregateId);
            
            uncommittedEvents = eventsRepository.GetUncommittedEvents();
            if (uncommittedEvents.Count > 0)
            {
                foreach (var id in uncommittedEvents)
                {
                    Guid aggregateId = id;
                    int lastSavedEventNumber = eventsStore.GetLastEventNumber(aggregateId);

                    eventsStore.SaveEvents(aggregateId, eventsRepository.Events, lastSavedEventNumber);
                }
            }

            foreach(var id in dAggregateId.Keys)
            {
                QueryModel model = new QueryModel(eventsStore, id);
                model.ShowProduct();

            }
        }

    }
}
