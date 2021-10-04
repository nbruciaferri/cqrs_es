using System;
using System.Collections.Generic;
using CQRS_ES.ES;

namespace CQRS_ES.Domain
{
    /// <summary>
    /// Actual implementation of event action
    /// </summary>
    public class ProductsAddedEvent : Event
    {
        public EventsRepository EventsRepository;
        private readonly Product _product;
        private readonly string _typeOfEvent;

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="eventsRepository"> Events repository to which events are added </param>
        /// <param name="product"> the Product interested in the event </param>
        /// <param name="quantity"> Quantity of the product </param>
        public ProductsAddedEvent(EventsRepository eventsRepository, Product product, int quantity)
        {
            EventsRepository = eventsRepository;
            Quantity = quantity;
            _product = product;
            AggregateId = _product.AggregateId;
            DateTime = DateTime.Now;
            _typeOfEvent = "Add";
        }

        /// <summary>
        /// Sets the event number to the event
        /// </summary>
        /// <param name="eventNumber"> The number to set </param>
        public override void SetEventNumber(int eventNumber)
        {
            EventNumber = eventNumber;
        }


        /// <summary>
        /// Adds the event to the Events Repository
        /// </summary>
        public override void Subscribe()
        {
            if (!EventsRepository.Events.ContainsKey(AggregateId))
                EventsRepository.Events.Add(AggregateId, new List<IEvent>());

            EventsRepository.Events[AggregateId].Add(this);
        }

        /// <summary>
        /// Override of the ToString() method to format the data as desired for the query
        /// </summary>
        /// <returns> The formatted string representation of the event </returns>
        public override string ToString()
        {
            return ($"Event {EventNumber}-{_product.AggregateId}: TYPE - {_typeOfEvent} - QUANTITY - {Quantity} - DATE - {DateTime}");
        }
    }
}
