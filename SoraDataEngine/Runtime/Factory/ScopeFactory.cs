using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoraDataEngine.Commons.Scopes;
using SoraDataEngine.Runtime.Manager;

namespace SoraDataEngine.Runtime.Factory
{
    public sealed class ScopeFactory
    {
        public ScopeFactory()
        {
        }

        public static IScope MakeScope(string name, IScope parent, IScope root, ScopeManager manager)
        {
            string id = Guid.NewGuid().ToString();
            return new Scope(name, id, string.Empty, parent.GetType(), root, parent, manager);
        }

        public static IScope MakeScope(string name, string id , IScope parent, IScope root, ScopeManager manager)
        {
            return new Scope(name, id, string.Empty, typeof(Scope), root, parent, manager);
        }

        public static T MakeScope<T>(string name, IScope parent, IScope root, ScopeManager manager) where T : IScope
        {
            return (T)MakeScope(name, parent, root, manager);
        }

        public static T MakeScopeCustom<T>(string name, IScope parent, IScope root, ScopeManager manager) where T : IScope
        {
            return (T)MakeScope(name, parent, root, manager);
        }

        public static T MakeScopeCustom<T>(string name, string id, IScope parent, IScope root, ScopeManager manager) where T : IScope
        {
            return (T)MakeScope(name, id, parent, root, manager);
        }
    }
}
