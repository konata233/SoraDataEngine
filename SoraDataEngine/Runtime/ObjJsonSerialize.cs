using SoraDataEngine.Commons.Scopes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime
{
    /// <summary>
    /// JSON 序列化
    /// 这个功能可能不是必需的，实际上二进制序列化已经足够
    /// 但是跨版本加载可能需要此功能
    /// </summary>
    public class ObjJsonSerialize
    {
        public static JsonArray SerializeAllScopes(IEnumerable<IScope> scopes)
        {
            JsonArray array = new JsonArray();
            foreach (IScope scope in scopes)
            {
                array.Add(scope);
            }
            return array;
        }
    }
}
