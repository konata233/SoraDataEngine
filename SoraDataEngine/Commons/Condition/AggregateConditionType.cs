using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Condition
{
    /// <summary>
    /// 条件类型
    /// </summary>
    public enum AggregateConditionType
    {
        /// <summary>
        /// 未指定
        /// </summary>
        NotDesignated,
        /// <summary>
        /// 任何一个都不满足
        /// </summary>
        None,
        /// <summary>
        /// 满足任何一个
        /// </summary>
        Any,
        /// <summary>
        /// 满足所有
        /// </summary>
        All
    }
}
