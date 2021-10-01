using System;
using System.Linq;
using CQRS_ES.Domain;
using CQRS_ES.ES;

namespace CQRS_ES.CQRS
{
    public class AddCommand : ICommand
    {
        public EventsRepository EventsRepository;
        public Product Product;
        public int Quantity;

        public AddCommand(EventsRepository eventsRepository, Guid aggregateId, int quantity, Product product)
        {
            EventsRepository = eventsRepository;
            Product = product;
            Quantity = quantity;
        }

        public void Apply()
        {
            var AddedEvent = new ProductsAddedEvent(EventsRepository, Product, Quantity);
            AddedEvent.Subscribe();
        }
    }
}
