using System;
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

        public void ScheduleEvent(int milisecondsDelay, long currentMiliSecond, IEvent @event)
        {
            var keyValuePair = new KeyValuePair<long, IEvent>(currentMiliSecond + milisecondsDelay, @event);
            _eventToHappen.Add(keyValuePair);
        }

        public void ScheduleEvent(int milisecondsDelay, long currentMiliSecond, Action action)
        {
            var @event = new EventInvokingAction(action);
            ScheduleEvent(milisecondsDelay, currentMiliSecond, @event);
        }

        public void Handle(long currentMiliSecond)
        {
            var eventsToRun = _eventToHappen
                .Where(kv => kv.Key <= currentMiliSecond)
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);

            foreach (var @event in eventsToRun)
            {
                @event.Run();
            }

            _eventToHappen.RemoveAll(kv => kv.Key <= currentMiliSecond);
        }
    }

    internal class EventInvokingAction : IEvent
    {
        private readonly Action _action;

        public EventInvokingAction(Action action)
        {
            _action = action;
        }

        public void Run()
        {
            _action();
        }
    }
}
