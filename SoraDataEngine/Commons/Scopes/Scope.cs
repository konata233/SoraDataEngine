using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SoraDataEngine.Commons;
using SoraDataEngine.Commons.Attributes;
using SoraDataEngine.Runtime;
using SoraDataEngine.Runtime.Factory;
using SoraDataEngine.Runtime.Manager;

namespace SoraDataEngine.Commons.Scopes
{
    public class Scope : IScope
    {
        public Scope(string name, string description, IScope? root, IScope? parent)
        {
            Name = name;
            FullName = parent != null ? parent.FullName + "." + name : name;
            ID = Guid.NewGuid().ToString();
            Description = description;
            Root = root;
            Parent = parent;
            ScopeType = typeof(IScope);
            Children = new List<IScope>();
            Attributes = new Dictionary<string, IAttribute>();
            IsRootScope = false;
            Manager = RuntimeCore.ScopeManager;
        }

        public Scope(string name, string id, string description, IScope? root, IScope? parent, ScopeManager manager)
        {
            Name = name;
            FullName = parent != null ? parent.FullName + "." + name : name;
            ID = id;
            Description = description;
            Root = root;
            Parent = parent;
            ScopeType = typeof(IScope);
            Children = new List<IScope>();
            Attributes = new Dictionary<string, IAttribute>();
            IsRootScope = false;
            Manager = manager;
        }

        public Scope(string name, string id, string description, Type scopeType, IScope? root, IScope? parent, ScopeManager manager)
        {
            Name = name;
            FullName = parent != null ? parent.FullName + "." + name : name;
            ID = id;
            Description = description;
            Root = root;
            Parent = parent;
            ScopeType = scopeType;
            Children = new List<IScope>();
            Attributes = new Dictionary<string, IAttribute>();
            IsRootScope = false;
            Manager = manager;
        }

        public Scope(string name, string id, string description, Type scopeType, IScope? root, IScope? parent, ScopeManager manager, bool isRootScope = false)
        {
            Name = name;
            FullName = parent != null ? parent.FullName + "." + name : name;
            ID = id;
            Description = description;
            Root = root;
            Parent = parent;
            ScopeType = scopeType;
            Children = new List<IScope>();
            Attributes = new Dictionary<string, IAttribute>();
            IsRootScope = isRootScope;
            Manager = manager;
        }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string ID { get; set; }

        public string Description { get; set; }

        public Type ScopeType { get; set; }

        public IScope? Root { get; set; }

        public IScope? Parent { get; set; }

        public List<IScope> Children { get; set; }

        public Dictionary<string, IAttribute> Attributes { get; set; }

        public bool IsRootScope { get; set; }

        public ScopeManager? Manager { get; set; }

        public IAttribute AddAttribute(IAttribute attribute)
        {
            return AddAttribute(attribute.Name, attribute);
        }

        public IAttribute AddAttribute(string name, IAttribute attribute)
        {
            if (Attributes.ContainsKey(name))
            {
                Attributes[name].Value = attribute.Value;
            }
            else
            {
                Attributes.Add(name, attribute);
            }
            return Attributes[name];
        }

        public IScope AddChild(IScope child)
        {
            Children.Add(child);
            return child;
        }

        public T AddChild<T>(T child) where T : IScope
        {
            if (child != null)
            {
                Children.Add((IScope)child);
            }
            else
            {
                throw new NullReferenceException(
                    Localization.GetLocalization("scope-addchild-nullreference")
                    ?? "Fail to add child to the scope! " + ToString()
                    );
            }
            return child;
        }

        public void AddChildren(List<IScope> children)
        {
            Children.AddRange(children);
        }

        public void AddChildren<T>(List<T> children) where T : IScope
        {
            if (children != null)
            {
                foreach (T child in children) AddChild(child);
            }
            else
            {
                throw new NullReferenceException(
                    Localization.GetLocalization("scope-addchildren-nullreference")
                    ?? "Fail to add children to the scope! " + ToString()
                    );
            }
        }

        public IAttribute? GetAttribute(string name)
        {
            Attributes.TryGetValue(name, out IAttribute? attribute);
            return attribute;
        }

        public T GetAttribute<T>() where T : IAttribute
        {
            throw new NotImplementedException();
        }

        public IScope? GetChild(string name)
        {
            return Children.FirstOrDefault((ss) => ss.Name == name);
        }

        public IScope? GetChildByFullName(string fullName)
        {
            return Children.FirstOrDefault(ss => ss.FullName == fullName);
        }

        public IScope? GetChildByID(string id)
        {
            return Children.FirstOrDefault((ss) => ss.ID == id);
        }

        public IScope? GetChildByName(string name)
        {
            return Children.FirstOrDefault(ss => ss.Name == name);
        }

        public List<IScope> GetChildren()
        {
            return Children;
        }

        public List<IScope> GetChildrenByType(Type type)
        {
            List<IScope> children = new List<IScope>();
            foreach (IScope child in Children)
            {
                if (type.IsAssignableFrom(child.GetType())) { children.Add(child); }
            }
            return children;
        }

        public List<T> GetChildrenByType<T>() where T : IScope
        {
            List<T> children = new List<T>();
            foreach (IScope child in Children)
            {
                if (typeof(T).IsAssignableFrom(child.GetType())) { children.Add((T)child); }
            }
            return children;
        }

        public IScope? GetFirstOrDefaultChildByType(Type type)
        {
            foreach (IScope child in Children)
            {
                if (type.IsAssignableFrom(child.GetType()))
                {
                    return child;
                }
            }
            return null;
        }

        public T? GetFirstOrDefaultChildByType<T>() where T : IScope
        {
            foreach (IScope child in Children)
            {
                if (typeof(T).IsAssignableFrom(child.GetType()))
                {
                    return (T)child;
                }
            }
            return default;
        }

        public IScope? GetParent()
        {
            return Parent;
        }

        public IScope GetRandomChild()
        {
            throw new NotImplementedException();
        }

        public IScope? GetRoot()
        {
            return Root;
        }

        public void RemoveAttribute(string name)
        {
            Attributes.Remove(name);
        }

        public void RemoveChild(IScope child)
        {
            foreach (IScope c in Children)
            {
                if (c.ID == child.ID)
                {
                    c.Dispose();
                    Children.Remove(c);
                }
            }
        }

        public void RemoveChildByFullName(string fullName)
        {
            foreach(IScope c in Children)
            {
                if (c.FullName == fullName)
                {
                    c.Dispose();
                    Children.Remove(c);
                }
            }
        }

        public void RemoveChildByID(string id)
        {
            foreach (IScope c in Children)
            {
                if (c.ID == ID)
                {
                    c.Dispose();
                    Children.Remove(c);
                }
            }
        }

        public void RemoveChildByName(string name)
        {
            foreach (IScope c in Children)
            {
                if (c.Name == name)
                {
                    c.Dispose();
                    Children.Remove(c);
                }
            }
        }

        public override string ToString()
        {
            return string.Concat("Scope: Name:", Name, 
                "FullName: ", FullName, 
                "ID: ", ID, 
                "Descr: ", Description, 
                "Parent: ", Parent?.Name);
        }

        public void Dispose()
        {
            Attributes.Clear();
            foreach (var c in Children)
            {
                if (c is not null) c.Dispose();
            }
            Children.Clear();
        }
    }
}
