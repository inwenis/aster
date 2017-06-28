using System;
using System.Collections.Generic;
using System.Linq;

namespace asterTake2
{
    internal class TimeBasedEvents
    {
        private readonly List<KeyValuePair<long, IEvent>> _eventToHappen;
        private List<KeyValuePair<long, Action>> _eventToHappen2;

        public TimeBasedEvents()
        {
            _eventToHappen = new List<KeyValuePair<long, IEvent>>();
            _eventToHappen2 = new List<KeyValuePair<long, Action>>();
        }

        public void ScheduleEvent(int milisecondsDelay, long currentMiliSecond, IEvent @event)
        {
            var keyValuePair = new KeyValuePair<long, IEvent>(currentMiliSecond + milisecondsDelay, @event);
            _eventToHappen.Add(keyValuePair);
        }

        public void ScheduleEvent(int milisecondsDelay, long currentMiliSecond, Action action)
        {
            var keyValuePair = new KeyValuePair<long, Action>(currentMiliSecond + milisecondsDelay, action);
            _eventToHappen2.Add(keyValuePair);
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

            var eventsToRun2 = _eventToHappen2
                .Where(kv => kv.Key <= currentMiliSecond)
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);

            foreach (var action in eventsToRun2)
            {
                action();
            }

            _eventToHappen2.RemoveAll(kv => kv.Key <= currentMiliSecond);
        }
    }
}