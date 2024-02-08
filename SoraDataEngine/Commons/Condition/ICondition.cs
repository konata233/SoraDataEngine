using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Condition
{
    public interface ICondition
    {
        /// <summary>
        /// 条件是否满足
        /// </summary>
        Func<bool> IsSatisfied { get; set; }
    }
}
