using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS_ES.ES;

namespace Web.UI.CQRS_ES.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsStore _eventStore;

        public EventsController(EventsStore eventsStore)
        {
            _eventStore = eventsStore;
        }

        // get: client
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowProductsByAggregateId(Guid aggregateId)
        {
            var eventsByAggregateId = _eventStore.GetEventsByAggregate(aggregateId);
            ViewData["NumEvents"] = eventsByAggregateId.Count;

            if (eventsByAggregateId.Count > 0)
            {
                foreach (var @event in eventsByAggregateId)
                {
                    ViewData["AggregateId"] = @event.ToString();
                }
            }

            return View();
        }
    }
}
