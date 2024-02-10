using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Timeline
{
    /// <summary>
    /// 时钟组件接口
    /// 只用于返回一个告知运算线程某时刻是否应当执行操作的布尔值
    /// 默认没有计时功能
    /// 在使用 IsClockFlipped 获取该布尔值后，如果原本为 True，IClock 应当将其翻转为 False
    /// 等到别处（或自身）调用 Flip() 方法再翻转为 True，由运算线程获取，再翻转为 False，等等
    /// IsClockFlipped不能恒为真！！
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// 是否在运行
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 是否已经翻转为 True
        /// </summary>
        bool IsClockFlipped { get; }

        /// <summary>
        /// 翻转时钟状态为 True
        /// </summary>
        void Flip();

        /// <summary>
        /// 启动时钟
        /// </summary>
        void Start();

        /// <summary>
        /// 停止时钟
        /// </summary>
        void Stop();
    }
}
