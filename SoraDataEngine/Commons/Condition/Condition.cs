using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Condition
{
    /// <summary>
    /// 基础条件
    /// </summary>
    public class Condition : ICondition
    {
        /// <summary>
        /// 检查是否满足条件的 Func
        /// </summary>
        public Func<bool> IsSatisfied {  get; set; }
        public Condition()
        {
            IsSatisfied = new Func<bool>(() => true);
        }
        public Condition(Func<bool> isSatisfied) {  this.IsSatisfied = isSatisfied; }
    }

    /// <summary>
    /// 默认返回 True 的条件
    /// </summary>
    public class TrueCondition : Condition
    {
        public TrueCondition()
        {
            IsSatisfied = new Func<bool>(() => true);
        }
    }

    /// <summary>
    /// 默认返回 False 的条件
    /// </summary>
    public class FalseCondition : Condition
    {
        public FalseCondition()
        {
            IsSatisfied = new Func<bool>(() => false);
        }
    }
}
