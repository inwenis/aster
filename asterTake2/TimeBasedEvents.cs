using System.Collections.Generic;
using System.Linq;

namespace asterTake2
{
    internal class TimeBasedEvents
    {
        private readonly List<KeyValuePair<long, IEvent>> _eventToHappen;

        public TimeBasedEvents()
        {
            _eventToHappen = new List<KeyValuePair<long, IEvent>>();
        }

        public void ScheduleEvent(int seconds, long currentMiliSecond, IEvent @event)
        {
            var keyValuePair = new KeyValuePair<long, IEvent>(currentMiliSecond + seconds*1000, @event);
            _eventToHappen.Add(keyValuePair);
        }

        public void Handle(long currentMiliSecond)
        {
            var eventsToRun = _eventToHappen
                .Where(kv => kv.Key <= currentMiliSecond)
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);

            foreach (var @event in eventsToRun)
            {
                @event.Run(currentMiliSecond);
            }

            _eventToHappen.RemoveAll(kv => kv.Key <= currentMiliSecond);
        }
    }
}