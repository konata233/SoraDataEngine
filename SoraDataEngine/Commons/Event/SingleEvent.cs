using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public class SingleEvent : IEvent
    {
        public ulong ExecuteTime { get; set; }
        public ICondition Condition { get; set; }
        public IEnumerable<IEffect> Effects { get; set; }

        public string EventID { get; set; }

        /// <summary>
        /// 创建计划事件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="effects">效果</param>
        /// <param name="executeTime">执行时间</param>
        public SingleEvent(ICondition condition, IEnumerable<IEffect> effects, ulong executeTime)
        {
            EventID = Guid.NewGuid().ToString();
            Effects = effects;
            ExecuteTime = executeTime;
            Condition = condition;
        }

        public bool Check(params object[] objects)
        {
            if (objects == null || objects.Length == 0) return false;
            if (objects[0].GetType() == typeof(ulong))
            {
                if ((ulong)objects[0] == ExecuteTime && Condition.IsSatisfied())
                {
                    Raise(objects);
                    return true;
                }
                else return false;
            }
            else return false;
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
