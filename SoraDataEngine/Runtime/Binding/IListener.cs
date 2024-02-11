using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    public interface IListener
    {
        string Name { get; set; }
        string ID { get; set; }
        Action<ulong, MessageCapsule> Callback { get; }
        void Receive(ulong time, MessageCapsule message);
    }
}
