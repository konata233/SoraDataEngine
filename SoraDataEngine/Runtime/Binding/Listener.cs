using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    /// <summary>
    /// 消息侦听器
    /// </summary>
    public class Listener : IListener
    {
        /// <summary>
        /// 消息侦听回调
        /// </summary>
        public Action<ulong, MessageCapsule> Callback {  get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="callback">回调</param>
        public Listener(string name, Action<ulong, MessageCapsule> callback) 
        { 
            Name = name;
            ID = Guid.NewGuid().ToString();
            Callback = callback;
        }

        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="message">消息</param>
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
