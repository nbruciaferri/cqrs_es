using System;
namespace CQRS_ES.Domain
{
    public class Product : IProduct
    {
        public Guid AggregateId { get; private set; }
        public string ProductId { get; private set; }
        public string Name { get => ProductId.Split('@')[0]; }
        public int Quantity { get; set; }
        private bool Available { get => Quantity > 0;}
        private decimal Price { get; set; }

        public Product(string name, Guid aggregateId, int quantity, decimal price)
        {
            ProductId = SetProductId(name, aggregateId);
            AggregateId = aggregateId;
            Quantity = quantity;
            Price = price;
        }

        public bool GetAvailability()
        {
            return Available;
        }

        public decimal GetPrice()
        {
            return Price;
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        private string SetProductId(string name, Guid aggregateId)
        {
            return $"{name}@{aggregateId}";
        }
    }
}
