using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SoraDataEngine.Commons.Scopes;
using SoraDataEngine.Runtime.Factory;
using Scope = SoraDataEngine.Commons.Scopes.Scope;

namespace SoraDataEngine.Runtime.Manager
{
    /// <summary>
    /// Scope 管理器
    /// </summary>
    public sealed class ScopeManager
    {
        private Dictionary<string, IScope> _scopes;
        private Dictionary<string, string> _scopesFullNameID;
        private IScope _rootScope;
        public static ScopeManager? Instance { get; private set; }

        public ScopeManager()
        {
            _scopes = new Dictionary<string, IScope>();
            _scopesFullNameID = new Dictionary<string, string>();

            _rootScope = new Scope("ROOT_SCOPE", Guid.NewGuid().ToString(), string.Empty, typeof(Scope), null, null, this, true);
            _scopes.Add(_rootScope.ID, _rootScope);
            _scopesFullNameID.Add(_rootScope.FullName, _rootScope.ID);

            Instance = RuntimeCore.ScopeManager;
        }

        /// <summary>
        /// 解析 Scopes（暂未实现！！）
        /// </summary>
        public void ResolveScopes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过 ID 获取 Scope
        /// </summary>
        /// <param name="id">Scope 的 ID</param>
        /// <returns>目标 Scope，不存在则为 null</returns>
        public IScope? GetScopeByID(string id)
        {
            _scopes.TryGetValue(id, out var scope);
            return scope;
        }
        /// <summary>
        /// 通过 名称 获取 Scope
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public IScope? GetScopeByName(string name, bool ignoreCase = false)
        {
            foreach (IScope scope in _scopes.Values)
            {
                if (scope.Name == name || (ignoreCase && scope.FullName.ToLower() == name.ToLower())) return scope;
            }
            return null;
        }

        /// <summary>
        /// 通过 全名 获取 Scope
        /// </summary>
        /// <param name="fullName">全名</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public IScope? GetScopeByFullName(string fullName, bool ignoreCase = false)
        {
            if (!ignoreCase)
            {
                if (_scopesFullNameID.ContainsKey(fullName)) 
                    return _scopes[_scopesFullNameID[fullName]];
            }
            else
            {
                foreach (IScope scope in _scopes.Values)
                {
                    if (ignoreCase && scope.FullName.ToLower() == fullName.ToLower()) 
                        return scope;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过 ID 删除 Scope
        /// </summary>
        /// <param name="id">ID</param>
        public void RemoveScopeByID(string id)
        {
            _scopes.Remove(id);
        }

        /// <summary>
        /// 通过 名称 删除 Scope
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        public void RemoveScopeByName(string name, bool ignoreCase = false)
        {
            foreach (IScope scope in _scopes.Values)
            {
                if (scope.Name == name || (ignoreCase && scope.FullName.ToLower() == name.ToLower())) 
                    _scopes.Remove(scope.ID);
                return;
            }
        }

        /// <summary>
        /// 通过 全名 删除 Scope
        /// </summary>
        /// <param name="fullName">全名</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        public void RemoveScopeByFullName(string fullName, bool ignoreCase = false)
        {
            if (!ignoreCase)
            {
                // 不忽略
                if (_scopes.ContainsKey(fullName))
                {
                    RemoveScopeByID(_scopesFullNameID[fullName]);
                }
            }
            else
            {
                foreach (IScope scope in _scopes.Values)
                {
                    if (ignoreCase && scope.FullName.ToLower() == fullName.ToLower()) 
                        _scopes.Remove(scope.ID);
                    return;
                }
            }
        }

        /// <summary>
        /// 是否具有 Scope
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public bool HasScopeByID(string id)
        {
            return _scopes.ContainsKey(id);
        }

        /// <summary>
        /// 是否具有 Scope - 名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public bool HasScopeByName(string name, bool ignoreCase = false)
        {
            foreach (IScope scope in _scopes.Values)
            {
                if (scope.Name == name || (ignoreCase && scope.Name.ToLower() == name.ToLower())) 
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否具有 Scope - 全名
        /// </summary>
        /// <param name="name">全名</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public bool HasScopeByFullName(string fullName, bool ignoreCase = false)
        {
            if (!ignoreCase)
            {
                return _scopesFullNameID.ContainsKey(fullName);
            }
            else
            {
                foreach (IScope scope in _scopes.Values)
                {
                    if (scope.FullName == fullName || (ignoreCase && scope.FullName.ToLower() == fullName.ToLower())) 
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 获取所有 Scope
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Scope?> GetScopes()
        {
            IEnumerable<Scope?> scopes = new List<Scope>();
            foreach (var scope in _scopes.Values)
            {
                scopes.Append(scope);
            }
            return scopes;
        }

        /// <summary>
        /// 添加 Scope
        /// </summary>
        /// <param name="scope">要添加的 Scope</param>
        /// <returns>添加的 Scope</returns>
        public IScope RegistScope(IScope scope)
        {
            if (_scopes.ContainsKey(scope.ID))
            {
                _scopes.Add(scope.ID, scope);
                _scopesFullNameID.Add(scope.FullName, scope.ID);
            }
            else
                _scopes[scope.ID] = scope;

            return scope;
        }

        /// <summary>
        /// 添加 Scope
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">父 Scope</param>
        /// <returns>添加的 Scope</returns>
        public IScope RegistScope(string name, IScope parent)
        {
            string id = Guid.NewGuid().ToString();
            IScope scope = ScopeFactory.MakeScope(name, parent, _rootScope, this);
            _scopes.Add(id, scope);
            _scopesFullNameID.Add(scope.FullName, id);
            return scope;
        }

        /// <summary>
        /// 获得根 Scope
        /// </summary>
        /// <returns></returns>
        public IScope GetRootScope()
        {
            return _rootScope;
        }
    }
}
