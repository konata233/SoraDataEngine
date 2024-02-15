using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev.Implements
{
    /// <summary>
    /// 基础树/链节点接口
    /// </summary>
    public interface INode
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
        /// 父节点
        /// </summary>
        INode? Parent { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        List<INode> Children { get; set; }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child">要添加的子节点</param>
        /// <returns></returns>
        INode AddChild(INode child);
        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="child">要移除的子节点</param>
        /// <returns></returns>
        INode RemoveChild(INode child);
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="id">子节点 ID</param>
        /// <returns></returns>
        INode? GetChildByID(string id);
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="name">子节点名称</param>
        /// <returns></returns>
        INode? GetChildByName(string name);
    }
}
