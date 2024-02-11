using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    public class Messenger : IDisposable
    {
        public static Messenger? Instance { get; set; }
        public Dictionary<string, IListener> Listeners { get; set; }

        private List<MessageCapsule> _messagesCache;

        private event Action<ulong, MessageCapsule>? ListenerReceived;

        public Messenger() 
        {
            Instance = RuntimeCore.Messenger;
            Listeners = new Dictionary<string, IListener>();
            _messagesCache = new List<MessageCapsule>();
        }

        public void Tick(ulong time)
        {
            foreach (var msg in _messagesCache)
            {
                ListenerReceived?.Invoke(time, msg);
            }
            _messagesCache.Clear();
        }

        public void SendMessage(MessageCapsule message)
        {
            _messagesCache.Add(message);
        }

        public void SendMessage(object sender, string target, params object[] messages)
        {
            _messagesCache.Add(new MessageCapsule(sender, target, messages));
        }

        public string RegistListener(IListener listener)
        {
            if (!Listeners.ContainsKey(listener.ID))
            {
                Listeners.Add(listener.ID, listener);
                ListenerReceived += listener.Callback;
            }
            else
            {
                ListenerReceived -= Listeners[listener.ID].Callback;
                Listeners[listener.ID] = listener;
                ListenerReceived += listener.Callback;
            }
            return listener.ID;
        }

        public bool RemoveListener(string id)
        {
            if (Listeners.ContainsKey(id))
            {
                ListenerReceived -= Listeners[id].Callback;
                Listeners.Remove(id);
                return true;
            }
            return false;
        }

        public bool RemoveListenerByName(string name)
        {
            foreach (var listener in Listeners.Values)
            {
                if (listener.Name == name)
                {
                    ListenerReceived -= listener.Callback;
                    Listeners.Remove(listener.ID);
                    return true;
                }
            }
            return false;
        }

        protected virtual void OnListenerReceived(ulong time, MessageCapsule capsule)
        {
            ListenerReceived?.Invoke(time, capsule);
        }

        public void Dispose()
        {
            Listeners?.Clear();
            _messagesCache.Clear();
        }
    }
}
