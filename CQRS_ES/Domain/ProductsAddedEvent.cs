using System;
using CQRS_ES.ES;

namespace CQRS_ES.Domain
{
    public class ProductsAddedEvent : Event
    {
        public EventsRepository EventsRepository;
        private readonly string TypeOfEvent;


        public ProductsAddedEvent(EventsRepository eventsRepository, Guid aggregateId, int quantity)
        {
            EventsRepository = eventsRepository;
            Quantity = quantity;
            AggregateId = aggregateId;
            DateTime = DateTime.Now;
            TypeOfEvent = "Add";
        }

        public override void SetEventNumber(int eventNumber)
        {
            EventNumber = eventNumber;
        }


        public override void Subscribe()
        {
            EventsRepository.Events.Add(this);
        }

        public override string ToString()
        {
            return ($"Event {EventNumber}-{AggregateId}: TYPE - {TypeOfEvent} - QUANTITY - {Quantity} - DATE - {DateTime}");
        }
    }
}
