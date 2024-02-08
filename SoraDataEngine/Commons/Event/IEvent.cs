using SoraDataEngine.Commons.Condition;
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
        IEnumerable<Action> Effects { get; set; }

        bool Check();
        void Raise();
    }
}
