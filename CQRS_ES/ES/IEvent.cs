using System;
namespace CQRS_ES.ES
{
    public interface IEvent
    {
        void SetEventNumber(int eventNumber);

        // The actual method where events are added to the EventsRepository
        void Subscribe();
    }
}
