using System;
using System.Collections.Generic;

namespace CQRS_ES.ES
{
    public interface IEventStore
    {
        int GetLastEventNumber(Guid aggregateId);
        List<Guid> GetSavedAggregateIds();
        List<IEvent> GetEventsByAggregate(Guid aggregateId);
        void SaveEvents(Guid aggregateId, Dictionary<Guid, List<IEvent>> events, int eventNumber);
    }
}
