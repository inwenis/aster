using System;
using System.Collections.Generic;
using System.Linq;

namespace asterTake2
{
    internal class TimeBasedActions
    {
        private readonly List<KeyValuePair<long, Action>> _actionsToRun;

        public TimeBasedActions()
        {
            _actionsToRun = new List<KeyValuePair<long, Action>>();
        }

        public void ScheduleAction(int milisecondsDelay, long currentMiliSecond, Action action)
        {
            var keyValuePair = new KeyValuePair<long, Action>(currentMiliSecond + milisecondsDelay, action);
            _actionsToRun.Add(keyValuePair);
        }

        public void Handle(long currentMiliSecond)
        {
            var actions = _actionsToRun
                .Where(kv => kv.Key <= currentMiliSecond)
                .OrderBy(kv => kv.Key)
                .Select(kv => kv.Value);

            foreach (var action in actions)
            {
                action();
            }

            _actionsToRun.RemoveAll(kv => kv.Key <= currentMiliSecond);
        }
    }
}
