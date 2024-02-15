using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev.Implements
{
    public class Node : INode, IDisposable
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public INode? Parent {  get; set; }
        public List<INode> Children {  get; set; }

        public Node(string name, INode? parent) 
        {
            Name = name;
            ID = Guid.NewGuid().ToString();
            Parent = parent;
            Children = new List<INode>();
        }

        public INode AddChild(INode child)
        {
            Children.Add(child);
            return child;
        }

        public INode RemoveChild(INode child)
        {
            Children.Remove(child);
            return child;
        }

        public INode? RemoveChildByName(string name)
        {
            INode? node = Children.Find(x => x.Name == name);
            if (node != null)
            {
                Children.Remove(node);
            }
            return node;
        }

        public INode? GetChildByName(string name)
        {
            return Children.Find(x => x.Name == name);
        }

        public INode? GetChildByID(string id)
        {
            return Children.Find(x => x.ID == id);
        }

        public void Dispose()
        {

        }
    }
}
