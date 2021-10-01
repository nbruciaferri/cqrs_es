using System;
namespace CQRS_ES.Domain
{
    public interface IProduct
    {
        int GetQuantity();
        bool GetAvailability();
        decimal GetPrice();
    }
}
