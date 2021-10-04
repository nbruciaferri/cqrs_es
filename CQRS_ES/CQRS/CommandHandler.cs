using CQRS_ES.Domain;
using CQRS_ES.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_ES.CQRS
{
    /// <summary>
    /// This class handles commands
    /// </summary>
    public class CommandHandler
    {
        private readonly EventsStore _eventsStore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventsStore"> The events store </param>
        public CommandHandler(EventsStore eventsStore)
        {
            _eventsStore = eventsStore;
        }

        /// <summary>
        /// Handles the commands based on the concrete type of the command
        /// </summary>
        /// <param name="command"> The command to handle </param>
        /// <param name="dAggregateId"> In memory cache of the actual product events to save </param>
        /// <returns></returns>
        public int HandleCommand(ICommand command, Dictionary<Guid, int> dAggregateId)
        {
            int retValue = -1;

            if (command.GetType().Equals(typeof(AddCommand)))
            {
                var c = command as AddCommand;
                int eventNumber = _eventsStore.GetLastEventNumber(c.Product.AggregateId);
                AddProcessedAggregateId(c.Product, eventNumber, dAggregateId);
                command.Apply();
                retValue = 1;
            }

            return retValue;
        }

        /// <summary>
        /// Adds the events to the in memory cache for later saving
        /// </summary>
        /// <param name="p"> The product </param>
        /// <param name="eventNumber"> The event number of the last saved event of that product </param>
        /// <param name="dAggregateId"> The in memory cache of the actual product events to save </param>
        private void AddProcessedAggregateId(Product p, int eventNumber, Dictionary<Guid, int> dAggregateId)
        {
            eventNumber = eventNumber++;
            if (!dAggregateId.Keys.Contains(p.AggregateId))
                dAggregateId.Add(p.AggregateId, eventNumber);

            else
            {
                dAggregateId[p.AggregateId] = eventNumber;
            }
        }

    }
}
