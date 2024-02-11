using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    public class MessageCapsule
    {
        public string Target {  get; set; }
        public object Sender { get; set; }
        public object[] Message { get; set; }

        public MessageCapsule(object sender, string target, params object[] message) 
        { 
            Target = target;
            Sender = sender;
            Message = message;
        }
    }
}
