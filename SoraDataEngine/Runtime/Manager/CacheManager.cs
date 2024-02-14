using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Manager
{
    public class CacheManager : IDisposable
    {
        public static CacheManager? Instance { get; private set; }
        private Hashtable _cache;

        public CacheManager() 
        {
            Instance = RuntimeCore.CacheManager;
            _cache = new Hashtable();
        }

        public void RemoveValue(string key)
        {
            _cache.Remove(key);
        }

        public object? GetValue(string key)
        {
            return _cache[key];
        }

        public void SetValue(string key, object? value)
        {
            _cache[key] = value;
        }

        public bool ContainsKey(string key)
        {
            return _cache.ContainsKey(key);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public void Dispose() 
        {
            _cache.Clear();
        }
    }
}
