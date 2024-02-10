using SoraDataEngine.Commons.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Effects
{
    public interface IEffect
    {
        ICondition Condition { get; set; }
        IEnumerable<Action> Actions { get; set; }
    }
}
