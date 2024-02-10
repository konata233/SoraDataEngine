using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using SoraDataEngine.Commons.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Factory
{
    /// <summary>
    /// 事件工厂
    /// </summary>
    public class EventFactory
    {
        public static IEvent MakeEvent(ICondition condition, IEnumerable<IEffect> effects)
        {
            return new Event(condition, effects);
        }
    }
}
