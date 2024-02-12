using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    /// <summary>
    /// 侦听器接口
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// 侦听回调
        /// </summary>
        Action<ulong, MessageCapsule> Callback { get; }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="time">时间</param>
        /// <param name="message">消息</param>
        void Receive(ulong time, MessageCapsule message);
    }
}
