using System;
using System.Linq;
using CQRS_ES.Domain;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    public class AddCommand : ICommand
    {
        public EventsRepository EventsRepository;
        public Guid AggregateId;
        public int Quantity;

        public AddCommand(EventsRepository eventsRepository, Guid aggregateId, int quantity)
        {
            EventsRepository = eventsRepository;
            AggregateId = aggregateId;
            Quantity = quantity;
        }

        public void Apply()
        {
            var AddedEvent = new ProductsAddedEvent(EventsRepository, AggregateId, Quantity);
            AddedEvent.Subscribe();
        }
    }
}
