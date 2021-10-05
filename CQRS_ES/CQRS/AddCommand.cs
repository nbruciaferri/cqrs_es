using System;
using System.Linq;
using CQRS_ES.Domain;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    /// <summary>
    /// Implementation of the Add command
    /// </summary>
    public class AddCommand : ICommand
    {
        private readonly EventsStore _eventsStore;
        public EventsRepository EventsRepository;
        public Product Product;
        public int Quantity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventsRepository"> The events repository to which adding the add event </param>
        /// <param name="product"> The product </param>
        /// <param name="quantity"> The quantity added to the product </param>
        public AddCommand(EventsStore eventsStore, EventsRepository eventsRepository, Product product, int quantity)
        {
            _eventsStore = eventsStore;
            EventsRepository = eventsRepository;
            Product = product;
            Quantity = quantity;
        }

        /// <summary>
        /// Adds the event to the events repository
        /// </summary>
        public void Apply()
        {
            var AddedEvent = new ProductsAddedEvent(_eventsStore, EventsRepository, Product, Quantity);
            AddedEvent.Subscribe();
        }
    }
}
