using SoraDataEngine.Runtime.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime
{
    public class RuntimeCore : IDisposable
    {
        public static RuntimeCore? Instance { get; private set; }
        public static ScopeManager? ScopeManager { get; private set; }

        public static bool IsCoreStarted { get; private set; } = false;
        
        public RuntimeCore()
        {
            ScopeManager = new ScopeManager();
            ScopeManager.ResolveScopes(); // not implemented yet!!
            IsCoreStarted = true;
            Instance = this;
        }

        public void Dispose()
        {
        }
    }
}
