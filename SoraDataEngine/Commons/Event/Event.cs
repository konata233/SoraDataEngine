using SoraDataEngine.Commons.Condition;
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
        public IEnumerable<Action> Effects { get; set; }

        public Event(ICondition condition, IEnumerable<Action> effects) 
        {
            Condition = condition;
            Effects = effects;
        }

        public bool Check()
        {
            if (Condition.IsSatisfied())
            {
                Raise();
                return true;
            }
            else { return false; }
        }

        public void Raise()
        {
            foreach (var action in Effects) 
            { 
                if (action is not null)
                {
                    action();
                }
            }
        }
    }
}
