using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoraDataEngine.Commons.Attributes;
using SoraDataEngine.Runtime.Factory;
using SoraDataEngine.Runtime.Manager;

namespace SoraDataEngine.Commons.Scopes
{
    public interface IScope
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 全名
        /// </summary>
        string FullName { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 是否是根 Scope
        /// </summary>
        bool IsRootScope { get; set; }
        /// <summary>
        /// Scope 的类型
        /// </summary>
        Type ScopeType { get; set; }
        /// <summary>
        /// Scope 所属的 ScopeManager
        /// </summary>
        ScopeManager Manager { get; }


        /// <summary>
        /// Scope 所属的根 Scope
        /// </summary>
        IScope? Root { get; set; }
        /// <summary>
        /// Scope 的父 Scope
        /// </summary>
        IScope? Parent { get; set; }
        /// <summary>
        /// Scope 的子 Scope
        /// </summary>
        List<IScope> Children { get; set; }
        /// <summary>
        /// Scope 的属性
        /// </summary>
        Dictionary<string, IAttribute> Attributes { get; set; }

        /// <summary>
        /// 添加一个子 Scope
        /// </summary>
        /// <param name="child"></param>
        void AddChild(IScope child);

        /// <summary>
        /// 添加一个子 Scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="child"></param>
        void AddChild<T>(T child) where T : IScope;

        /// <summary>
        /// 添加多个子 Scope
        /// </summary>
        /// <param name="children"></param>
        void AddChildren(List<IScope> children);
        /// <summary>
        /// 添加多个子 Scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="children"></param>
        void AddChildren<T>(List<T> children) where T : IScope;

        /// <summary>
        /// 移除子 Scope
        /// </summary>
        /// <param name="child"></param>
        void RemoveChild(IScope child);
        /// <summary>
        /// 移除子 Scope
        /// </summary>
        /// <param name="name"></param>
        void RemoveChildByName(string name);
        /// <summary>
        /// 移除子 Scope
        /// </summary>
        /// <param name="fullName"></param>
        void RemoveChildByFullName(string fullName);
        /// <summary>
        /// 移除子 Scope
        /// </summary>
        /// <param name="id"></param>
        void RemoveChildByID(string id);

        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IScope? GetChild(string name);
        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IScope? GetChildByID(string id);
        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IScope? GetChildByName(string name);
        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IScope? GetChildByFullName(string fullName);

        /// <summary>
        /// 获取所有子 Scope
        /// </summary>
        /// <returns></returns>
        List<IScope> GetChildren();

        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IScope? GetFirstOrDefaultChildByType(Type type);
        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T? GetFirstOrDefaultChildByType<T>() where T : IScope;

        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<IScope> GetChildrenByType(Type type);
        /// <summary>
        /// 获取子 Scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetChildrenByType<T>() where T : IScope;

        IScope GetRandomChild();

        /// <summary>
        /// 获取父 Scope
        /// </summary>
        /// <returns></returns>
        IScope? GetParent();
        /// <summary>
        /// 获取根 Scope
        /// </summary>
        /// <returns></returns>
        IScope? GetRoot();

        /// <summary>
        /// 添加或者更新 Attribute
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        IAttribute AddAttribute(string name, IAttribute value);
        /// <summary>
        /// 获取 Attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAttribute? GetAttribute(string name);
        /// <summary>
        /// 获取 Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetAttribute<T>() where T : IAttribute;
        /// <summary>
        /// 移除 Attribute
        /// </summary>
        /// <param name="name"></param>
        void RemoveAttribute(string name);
    }
}
