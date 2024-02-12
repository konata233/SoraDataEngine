using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev
{
    /// <summary>
    /// 插件入口接口
    /// </summary>
    public interface IEntry
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
