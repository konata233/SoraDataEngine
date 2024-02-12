using SoraDataEngine.Commons.Dev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Loader
{
    /// <summary>
    /// 程序集插件加载器
    /// </summary>
    public class AsmLoader
    {
        /// <summary>
        /// 此类的实例
        /// </summary>
        public static AsmLoader? Instance { get; private set; }
        private List<Assembly> _assemblyList = new List<Assembly>();
        private List<(IEntry, IEntryConfig)> _entries = new List<(IEntry, IEntryConfig)>();

        /// <summary>
        /// 实例化此类
        /// </summary>
        /// <param name="config">配置</param>
        public AsmLoader(AsmLoaderConfig config) 
        {
            Instance = RuntimeCore.AsmLoader;
            string[] files = Directory.GetFiles(config.AsmLoadPath, config.AsmSearchPattern);
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    _assemblyList.Add(Assembly.LoadFrom(file));
                }
            }
        }

        /// <summary>
        /// 加载所有插件
        /// </summary>
        public void LoadAllEntries()
        {
            foreach (Assembly asm in _assemblyList)
            {
                Type[] types = asm.GetTypes();
                foreach (Type type in types)
                {
                    IEntry? entry = null;
                    IEntryConfig? config = null;

                    if (typeof(IEntry).IsAssignableFrom(type))
                    {
                        entry = (IEntry?)asm.CreateInstance(type.FullName, true);
                    }
                    if (typeof(IEntryConfig).IsAssignableFrom(type))
                    {
                        config = (IEntryConfig?)asm.CreateInstance(type.FullName, true);
                    }
                    if (entry != null && config != null)
                    {
                        _entries.Add((entry, config));
                    }
                }
            }
        }

        /// <summary>
        /// 按 StartLevel 从小到大顺序启动所有插件程序集
        /// </summary>
        public void StartAllEntries()
        {
            IEnumerable<IEntry> query = 
                from entry in _entries
                orderby entry.Item2.StartLevel ascending
                select entry.Item1;
            foreach (IEntry value in query)
            {
                value.Start();
            }
        }

        /// <summary>
        /// 按 StartLevel 从大到小终止所有插件程序集
        /// </summary>
        public void StopAllEntries()
        {
            IEnumerable<IEntry> query =
                from entry in _entries
                orderby entry.Item2.StartLevel descending
                select entry.Item1;
            foreach (IEntry value in query)
            {
                value.Stop();
            }
        }
    }
}
