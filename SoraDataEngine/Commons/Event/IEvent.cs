using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public interface IEvent
    {
        ICondition Condition { get; set; }
        IEnumerable<IEffect> Effects { get; set; }
        string EventID { get; }

        bool Check(params object[] objects);
        void Raise(params object[] objects);
    }
}
