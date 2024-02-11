using SoraDataEngine.Runtime.Binding;
using SoraDataEngine.Runtime.Manager;
using SoraDataEngine.Runtime.Timeline;
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
        public static EventManager? EventManager { get; private set; }
        public static Scheduler? Scheduler { get; private set; }
        public static Messenger? Messenger { get; private set; }
        public static AttributeBinder? AttributeBinder { get; private set; }

        public static bool IsCoreStarted { get; private set; } = false;
        
        public RuntimeCore()
        {
            ScopeManager = new ScopeManager();
            ScopeManager.ResolveScopes(); // not implemented yet!!

            EventManager = new EventManager();
            Scheduler = new Scheduler();

            Messenger = new Messenger();

            AttributeBinder = new AttributeBinder();

            IsCoreStarted = false;
            Instance = this;
        }

        public RuntimeCore(IClock clock)
        {
            ScopeManager = new ScopeManager();
            ScopeManager.ResolveScopes(); // not implemented yet!!

            EventManager = new EventManager();
            Scheduler = new Scheduler(clock);

            IsCoreStarted = false;
            Instance = this;
        }

        public void Start()
        {
            Scheduler?.Start();
            IsCoreStarted = true;
        }

        public void Dispose()
        {
        }
    }
}
