using System;
using System.Collections.Generic;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    /// <summary>
    /// Query model to show data to the client
    /// </summary>
    public class QueryModel
    {
        private readonly EventsStore _eventsStore;
        private readonly Guid _aggregateId;
        //private int _totalQuantity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventsStore"> The events store to retrieve data </param>
        /// <param name="aggregateId"> The aggregate id used to filter results </param>
        public QueryModel(EventsStore eventsStore, Guid aggregateId)
        {
            _eventsStore = eventsStore;
            _aggregateId = aggregateId;
            //_totalQuantity = 0;
        }

        /// <summary>
        /// Shows data to the client
        /// </summary>
        public void ShowProduct()
        {
            //_totalQuantity = GetTotalQuantity();
            foreach (var @event in _eventsStore.GetEventsByAggregate(_aggregateId))
                Console.WriteLine(@event.ToString());
            //+$"- AVAILABLE - {GetProductAvailability()} - TOTAL QUANTITY - {(_totalQuantity > 0 ? _totalQuantity : 0)}
        }

        /// <summary>
        /// Gets total quantity for the desired aggregateId
        /// </summary>
        /// <returns> The total quantity of the product </returns>
        //private int GetTotalQuantity()
        //{
        //    _eventsStore.GetEventsByAggregate(_aggregateId).ForEach(x => _totalQuantity += ((Event)x).Quantity);
        //    return _totalQuantity;
        //}

        //private bool GetProductAvailability()
        //{
        //    return _totalQuantity > 0;
        //}
    }
}
