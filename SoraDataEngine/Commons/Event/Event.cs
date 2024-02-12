using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    /// <summary>
    /// 基础事件类
    /// </summary>
    public class Event : IEvent
    {
        /// <summary>
        /// 条件
        /// </summary>
        public ICondition Condition {  get; set; }
        /// <summary>
        /// 效果
        /// </summary>
        public IEnumerable<IEffect> Effects { get; set; }
        /// <summary>
        /// 事件 ID
        /// </summary>
        public string EventID { get; private set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="effects">效果</param>
        public Event(ICondition condition, IEnumerable<IEffect> effects) 
        {
            Condition = condition;
            Effects = effects;
            EventID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 检查是否满足条件
        /// </summary>
        /// <param name="objects">参数</param>
        /// <returns>是否满足条件</returns>
        public bool Check(params object[] objects)
        {
            if (Condition.IsSatisfied())
            {
                Raise(objects);
                return true;
            }
            else { return false; }
        }

        public void Raise(params object[] objects)
        {
            foreach (var effect in Effects) 
            { 
                if (effect is not null)
                {
                    foreach (var action in effect.Actions)
                    {
                        if (action is not null)
                        {
                            action((ulong)objects[0]);
                        }
                    }
                }
            }
        }
    }
}
