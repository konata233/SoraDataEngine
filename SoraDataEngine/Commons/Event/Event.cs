using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public class Event : IEvent
    {
        public ICondition Condition {  get; set; }
        public IEnumerable<IEffect> Effects { get; set; }
        public string EventID { get; private set; }

        public Event(ICondition condition, IEnumerable<IEffect> effects) 
        {
            Condition = condition;
            Effects = effects;
            EventID = Guid.NewGuid().ToString();
        }

        public bool Check(params object[] objects)
        {
            if (Condition.IsSatisfied())
            {
                Raise(objects);
                return true;
            }
            else { return false; }
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
