using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public class ScheduledEvent : IEvent
    {
        public ulong ScheduledTime { get; set; }
        public ICondition Condition { get; set; }
        public IEnumerable<IEffect> Effects { get; set; }

        public string EventID {  get; set; }

        public ScheduledEvent(ICondition condition, IEnumerable<IEffect> effects, ulong scheduledTime)
        {
            EventID = Guid.NewGuid().ToString();
            Effects = effects;
            ScheduledTime = scheduledTime;
            Condition = condition;
        }

        public bool Check(params object[] objects)
        {
            if (objects == null || objects.Length == 0) return false;
            if (objects[0].GetType() == typeof(ulong))
            {
                if ((ulong)objects[0] >= ScheduledTime && Condition.IsSatisfied())
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
                            action();
                        }
                    }
                }
            }
        }
    }
}
