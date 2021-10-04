using System;
using System.Collections.Generic;
using CQRS_ES.ES;

namespace CQRS_ES.Domain
{
    public class ProductsAddedEvent : Event
    {
        public EventsRepository EventsRepository;
        private readonly Product _product;
        private readonly string _typeOfEvent;


        public ProductsAddedEvent(EventsRepository eventsRepository, Product product, int quantity)
        {
            EventsRepository = eventsRepository;
            Quantity = quantity;
            _product = product;
            AggregateId = _product.AggregateId;
            DateTime = DateTime.Now;
            _typeOfEvent = "Add";
        }

        public override void SetEventNumber(int eventNumber)
        {
            EventNumber = eventNumber;
        }


        public override void Subscribe()
        {
            if (!EventsRepository.Events.ContainsKey(AggregateId))
                EventsRepository.Events.Add(AggregateId, new List<IEvent>());

            EventsRepository.Events[AggregateId].Add(this);
        }

        public override string ToString()
        {
            return ($"Event {EventNumber}-{_product.AggregateId}: TYPE - {_typeOfEvent} - QUANTITY - {Quantity} - DATE - {DateTime}");
        }
    }
}
