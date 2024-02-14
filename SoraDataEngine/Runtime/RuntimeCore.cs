using SoraDataEngine.Runtime.Binding;
using SoraDataEngine.Runtime.Loader;
using SoraDataEngine.Runtime.Manager;
using SoraDataEngine.Runtime.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime
{
    [Serializable]
    public class RuntimeCore : IDisposable
    {
        public static AsmLoader? AsmLoader { get; private set; }
        public static RuntimeCore? Instance { get; private set; }
        public static ScopeManager? ScopeManager { get; private set; }
        public static EventManager? EventManager { get; private set; }
        public static Scheduler? Scheduler { get; private set; }
        public static Messenger? Messenger { get; private set; }
        public static AttributeBinder? AttributeBinder { get; private set; }
        public static CacheManager? CacheManager { get; private set; }

        public static bool IsCoreStarted { get; private set; } = false;
        
        public RuntimeCore(AsmLoaderConfig loaderConfig)
        {
            AsmLoader = new AsmLoader(loaderConfig);
            ScopeManager = new ScopeManager();

            EventManager = new EventManager();
            Scheduler = new Scheduler();

            Messenger = new Messenger();
            AttributeBinder = new AttributeBinder();
            CacheManager = new CacheManager();

            IsCoreStarted = false;
            Instance = this;
        }

        public RuntimeCore(AsmLoaderConfig loaderConfig, IClock clock) : this(loaderConfig)
        {
            Scheduler = new Scheduler(clock);
        }

        /// <summary>
        /// 启动运行时
        /// </summary>
        public void Start()
        {
            AsmLoader?.StartAllEntries();
            Scheduler?.Start();
            IsCoreStarted = true;
        }

        /// <summary>
        /// 暂停运行时
        /// </summary>
        public void Pause()
        {
            Scheduler?.Pause();
        }

        /// <summary>
        /// 从暂停状态恢复运行时
        /// </summary>
        public void Restart()
        {
            Scheduler?.Restart();
        }

        /// <summary>
        /// 终止运行时
        /// </summary>
        public void Stop()
        {
            Scheduler?.Stop();
            AsmLoader?.StopAllEntries();
        }

        /// <summary>
        /// 销毁运行时
        /// </summary>
        public void Dispose()
        {
            Scheduler?.Dispose();
            Messenger?.Dispose();
            IsCoreStarted = false;
        }
    }
}
