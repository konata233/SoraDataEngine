using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev
{
    /// <summary>
    /// 插件入口配置接口
    /// </summary>
    public interface IEntryConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 启动级别
        /// 越小越早启动
        /// </summary>
        int StartLevel { get; set; }
        /// <summary>
        /// 依赖项
        /// </summary>
        string[] Dependencies { get; set; }
    }
}
