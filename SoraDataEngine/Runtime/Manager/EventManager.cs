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

        public EventManager() 
        {
            Instance = RuntimeCore.EventManager;
            events = new Dictionary<string, IEvent>();
        }

        public void RegistEvent(IEvent @event)
        {
            events.Add(@event.EventID, @event);
        }

        public void RemoveEvent(string @eventID)
        {
            events.Remove(@eventID);
        }

        public void Tick(ulong currentTime)
        {
            foreach (var @event in events.Values)
            {
                @event.Check(currentTime);
            }
        }
    }
}
