using System;
using CQRS_ES.Domain;

namespace CQRS_ES.ES
{
    public abstract class Event : IEvent
    {
        public int EventNumber;

        public int Quantity;

        public Guid AggregateId;

        public int TotalQuantity;

        protected DateTime DateTime;

        public abstract void SetEventNumber(int eventNumber);

        public abstract void Subscribe();

        public abstract override string ToString();
    }
}
