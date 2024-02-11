using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Event
{
    public class ScheduledEvent : IEvent
    {
        public ulong StartTime { get; set; }
        public ulong EndTime { get; set; }
        public ICondition Condition { get; set; }
        public IEnumerable<IEffect> Effects { get; set; }

        public string EventID {  get; set; }

        /// <summary>
        /// 创建计划事件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="effects">效果</param>
        /// <param name="startTime">开始时间（闭区间）</param>
        /// <param name="endTime">结束时间（闭区间！！！）</param>
        public ScheduledEvent(ICondition condition, IEnumerable<IEffect> effects, ulong startTime, ulong endTime)
        {
            EventID = Guid.NewGuid().ToString();
            Effects = effects;
            StartTime = startTime;
            EndTime = endTime;
            Condition = condition;
        }

        public bool Check(params object[] objects)
        {
            if (objects == null || objects.Length == 0) return false;
            if (objects[0].GetType() == typeof(ulong))
            {
                if ((ulong)objects[0] >= StartTime && ((ulong)objects[0]) <= EndTime && Condition.IsSatisfied())
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
