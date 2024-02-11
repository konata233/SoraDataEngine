using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    public class Listener : IListener
    {
        public Action<ulong, MessageCapsule> Callback {  get; set; }
        public string Name {  get; set; }
        public string ID { get; set; }

        public Listener(string name, Action<ulong, MessageCapsule> callback) 
        { 
            Name = name;
            ID = Guid.NewGuid().ToString();
            Callback = callback;
        }

        public void Receive(ulong time, MessageCapsule message)
        {
            var target = message.Target;
            if (Name == target || ID == target)
            {
                Callback(time, message);
            }
        }
    }
}
