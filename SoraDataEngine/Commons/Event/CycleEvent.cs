using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public class CycleEvent : IEvent
    {
        public ICondition Condition { get; set; }
        public IEnumerable<IEffect> Effects { get; set; }

        public string EventID {  get; private set; }

        public ulong StartTime { get; set; }
        public ulong EndTime { get; set; }
        public ulong Interval { get; set; }

        private ulong _lastActivatedTime;

        public CycleEvent(ICondition condition, IEnumerable<IEffect> effects, ulong startTime, ulong endTime, ulong interval)
        {
            Condition = condition;
            Effects = effects;
            EventID = Guid.NewGuid().ToString();

            StartTime = startTime; 
            EndTime = endTime;
            Interval = interval;

            _lastActivatedTime = startTime;
        }

        public bool Check(params object[] objects)
        {
            if (objects == null || objects.Length == 0) return false;
            if (objects[0].GetType() == typeof(ulong))
            {
                ulong time = (ulong)objects[0];

                if (time >= StartTime && time <= EndTime && 
                    (time - _lastActivatedTime) >= Interval && 
                    Condition.IsSatisfied())
                {
                    Raise(objects);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void Raise(params object[] objects)
        {
            foreach (var effect in Effects)
            {
                if (effect is not null)
                {
                    foreach (var action in effect.Actions)
                    {
                        if (action is not null)
                        {
                            action((ulong)objects[0]);
                        }
                    }
                }
            }
        }
    }
}
