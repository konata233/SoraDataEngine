using SoraDataEngine.Commons.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Effects
{
    /// <summary>
    /// 基础效果
    /// </summary>
    public class Effect : IEffect
    {
        /// <summary>
        /// 条件
        /// </summary>
        public ICondition Condition { get; set; }
        /// <summary>
        /// 要执行的操作
        /// </summary>
        public IEnumerable<Action<ulong>> Actions { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="actions">效果</param>
        public Effect(ICondition condition, IEnumerable<Action<ulong>> actions) 
        {
            Condition = condition;
            Actions = actions;
        }
    }
}
