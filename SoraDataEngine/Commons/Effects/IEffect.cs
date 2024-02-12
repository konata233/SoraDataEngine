using SoraDataEngine.Commons.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Effects
{
    /// <summary>
    /// 效果接口
    /// </summary>
    public interface IEffect
    {
        /// <summary>
        /// 条件
        /// </summary>
        ICondition Condition { get; set; }
        /// <summary>
        /// 要执行的操作
        /// </summary>
        IEnumerable<Action<ulong>> Actions { get; set; }
    }
}
