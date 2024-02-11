using SoraDataEngine.Commons.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Effects
{
    public class Effect : IEffect
    {
        public ICondition Condition { get; set; }
        public IEnumerable<Action<ulong>> Actions { get; set; }

        public Effect(ICondition condition, IEnumerable<Action<ulong>> actions) 
        {
            Condition = condition;
            Actions = actions;
        }
    }
}
