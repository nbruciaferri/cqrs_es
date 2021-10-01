using System;
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
            DateTime = DateTime.Now;
            _typeOfEvent = "Add";
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
            return ($"Event {EventNumber}-{_product.AggregateId}: TYPE - {_typeOfEvent} - QUANTITY - {Quantity} - DATE - {DateTime}");
        }
    }
}
