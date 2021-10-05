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
        private readonly EventsStore _eventsStore;
        public EventsRepository EventsRepository;
        private readonly Product _product;
        private readonly string _typeOfEvent;

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="eventsRepository"> Events repository to which events are added </param>
        /// <param name="product"> the Product interested in the event </param>
        /// <param name="quantity"> Quantity of the product </param>
        public ProductsAddedEvent(EventsStore eventsStore, EventsRepository eventsRepository, Product product, int quantity)
        {
            _eventsStore = eventsStore;
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



        public void SetTotalQuantity()
        {
            _eventsStore.GetEventsByAggregate(AggregateId).ForEach(x => TotalQuantity += ((Event)x).Quantity);
            TotalQuantity = TotalQuantity + Quantity;

            if (TotalQuantity < 0)
                TotalQuantity = 0;
        }

        public bool GetAvailability()
        {
            return TotalQuantity > 0;
        }

        /// <summary>
        /// Adds the event to the Events Repository
        /// </summary>
        public override void Subscribe()
        {
            SetTotalQuantity();
            if (TotalQuantity >= 0)
            {
                if (!EventsRepository.Events.ContainsKey(AggregateId))
                    EventsRepository.Events.Add(AggregateId, new List<IEvent>());

                EventsRepository.Events[AggregateId].Add(this);
            }
        }

        /// <summary>
        /// Override of the ToString() method to format the data as desired for the query
        /// </summary>
        /// <returns> The formatted string representation of the event </returns>
        public override string ToString()
        {
            return ($"Event {EventNumber}-{_product.AggregateId}: \n" +
                $"\tTYPE: {_typeOfEvent} \n" +
                $"\tQUANTITY: {Quantity} \n" +
                $"\tDATE: {DateTime} \n" +
                $"\tAVAILABLE: {GetAvailability()}\n" +
                $"\tTOTAL QUANTITY: {TotalQuantity}\n");
        }
    }
}
