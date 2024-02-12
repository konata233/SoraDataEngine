using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    /// <summary>
    /// 消息封装
    /// </summary>
    public class MessageCapsule
    {
        /// <summary>
        /// 目标
        /// </summary>
        public string Target {  get; set; }
        /// <summary>
        /// 发送方实例
        /// </summary>
        public object Sender { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public object[] Message { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="sender">发送方实例</param>
        /// <param name="target">目标</param>
        /// <param name="message">消息内容</param>
        public MessageCapsule(object sender, string target, params object[] message) 
        { 
            Target = target;
            Sender = sender;
            Message = message;
        }
    }
}
