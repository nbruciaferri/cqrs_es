using System;
namespace CQRS_ES.Domain
{
    public interface IProduct
    {
        bool GetAvailability();
        decimal GetPrice();
    }
}
