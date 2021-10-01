using System;
using System.Collections.Generic;

namespace CQRS_ES.ES
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, List<IEvent> events, int eventNumber);
        List<IEvent> GetEventsByAggregate(Guid aggregateId);
    }
}
