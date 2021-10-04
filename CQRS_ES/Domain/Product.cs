using System;
namespace CQRS_ES.Domain
{
    /// <summary>
    /// Product class
    /// </summary>
    public class Product : IProduct
    {
        /// <summary>
        /// Unique Id for each product
        /// </summary>
        public Guid AggregateId { get; private set; }
        /// <summary>
        /// The combination of the product name and unique id
        /// </summary>
        public string ProductId { get; private set; }
        /// <summary>
        /// The product's name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Actual quantity of the product
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// If the product is available or not based on the quantity
        /// </summary>
        private bool Available { get => Quantity > 0;}
        /// <summary>
        /// Product's price
        /// </summary>
        private decimal Price { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"> Product name </param>
        /// <param name="aggregateId"> Product aggregateId </param>
        /// <param name="quantity"> Product quantity </param>
        /// <param name="price"> Product price </param>
        public Product(string name, Guid aggregateId, int quantity, decimal price)
        {
            ProductId = SetProductId(name, aggregateId);
            Name = name;
            AggregateId = aggregateId;
            Quantity = quantity;
            Price = price;
        }

        /// <summary>
        /// Gets the availability of the product
        /// </summary>
        /// <returns></returns>
        public bool GetAvailability()
        {
            return Available;
        }

        /// <summary>
        /// Gets the price of the product
        /// </summary>
        /// <returns></returns>
        public decimal GetPrice()
        {
            return Price;
        }


        /// <summary>
        /// Sets the product's id
        /// </summary>
        /// <param name="name"> The product's name </param>
        /// <param name="aggregateId"> The product's aggregate id </param>
        /// <returns></returns>
        private string SetProductId(string name, Guid aggregateId)
        {
            return $"{name}@{aggregateId}";
        }
    }
}
