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
    /// 循环事件
    /// </summary>
    public class CycleEvent : IEvent
    {
        /// <summary>
        /// 条件
        /// </summary>
        public ICondition Condition { get; set; }
        /// <summary>
        /// 效果
        /// </summary>
        public IEnumerable<IEffect> Effects { get; set; }

        /// <summary>
        /// 事件 ID
        /// </summary>
        public string EventID {  get; private set; }

        /// <summary>
        /// 启动时间
        /// </summary>
        public ulong StartTime { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public ulong EndTime { get; set; }
        /// <summary>
        /// 间隔时间
        /// </summary>
        public ulong Interval { get; set; }

        /// <summary>
        /// 上一次激活的时间
        /// </summary>
        private ulong _lastActivatedTime;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="effects">效果</param>
        /// <param name="startTime">启动时间（含）</param>
        /// <param name="endTime">终止时间（可能含）</param>
        /// <param name="interval">间隔时间</param>
        public CycleEvent(ICondition condition, IEnumerable<IEffect> effects, ulong startTime, ulong endTime, ulong interval)
        {
            Condition = condition;
            Effects = effects;
            EventID = Guid.NewGuid().ToString();

            StartTime = startTime; 
            EndTime = endTime;
            Interval = interval;

            _lastActivatedTime = startTime;
        }

        /// <summary>
        /// 检查是否满足条件
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public bool Check(params object[] objects)
        {
            if (objects == null || objects.Length == 0) return false;
            if (objects[0].GetType() == typeof(ulong))
            {
                ulong time = (ulong)objects[0];

                if (time >= StartTime && time <= EndTime && 
                    (time - _lastActivatedTime) >= Interval && 
                    Condition.IsSatisfied())
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
