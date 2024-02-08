using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Condition
{
    public class Condition : ICondition
    {
        public Func<bool> IsSatisfied {  get; set; }
        public Condition()
        {
            IsSatisfied = new Func<bool>(() => true);
        }
        public Condition(Func<bool> isSatisfied) {  this.IsSatisfied = isSatisfied; }
    }
}
