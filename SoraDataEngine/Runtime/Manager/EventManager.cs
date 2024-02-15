using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Manager
{
    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager
    {
        public static EventManager? Instance { get; private set; }
        private Dictionary<string, IEvent> events;

        /// <summary>
        /// 建议使用，避免重复创建 TrueCondition 实例
        /// </summary>
        public static TrueCondition TrueConditionInst {  get; private set; } = new TrueCondition();
        /// <summary>
        /// 建议使用，避免重复创建 FalseCondition 实例
        /// </summary>
        public static FalseCondition FalseConditionInst { get; private set; } = new FalseCondition();

        public EventManager() 
        {
            Instance = RuntimeCore.EventManager;
            events = new Dictionary<string, IEvent>();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="event">要注册的事件</param>
        /// <returns>事件 ID</returns>
        public string RegistEvent(IEvent @event)
        {
            events.Add(@event.EventID, @event);
            return @event.EventID;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventID">事件 ID</param>
        public void RemoveEvent(string @eventID)
        {
            events.Remove(@eventID);
        }

        /// <summary>
        /// 随机刻更新调用
        /// </summary>
        /// <param name="currentTime"></param>
        internal Task Tick(ulong currentTime)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var @event in events.Values)
                {
                    @event.Check(currentTime);
                }
            });
        }
    }
}
