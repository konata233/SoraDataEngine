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
        string Name { get; set; }
        string FullName { get; set; }
        string ID { get; set; }
        string Description { get; set; }
        bool IsRootScope { get; set; }
        Type ScopeType { get; set; }
        ScopeManager Manager { get; }

        IScope? Root { get; set; }
        IScope? Parent { get; set; }
        List<IScope> Children { get; set; }
        Dictionary<string, IAttribute> Attributes { get; set; }

        void AddChild(IScope child);
        void AddChild<T>(T child) where T : IScope;

        void AddChildren(List<IScope> children);
        void AddChildren<T>(List<T> children) where T : IScope;

        void RemoveChild(IScope child);
        void RemoveChildByName(string name);
        void RemoveChildByID(string id);

        IScope? GetChild(string name);
        IScope? GetChildByID(string id);

        List<IScope> GetChildren();

        IScope? GetFirstOrDefaultChildByType(Type type);
        T? GetFirstOrDefaultChildByType<T>() where T : IScope;

        List<IScope> GetChildrenByType(Type type);
        List<T> GetChildrenByType<T>() where T : IScope;

        IScope GetRandomChild();

        IScope? GetParent();
        IScope? GetRoot();

        void AddAttribute(string name, IAttribute value);
        IAttribute? GetAttribute(string name);
        T GetAttribute<T>() where T : IAttribute;
        void RemoveAttribute(string name);
    }
}
