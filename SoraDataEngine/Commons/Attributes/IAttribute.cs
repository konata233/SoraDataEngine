using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Attributes
{
    /// <summary>
    /// 属性接口
    /// </summary>
    public interface IAttribute
    {
        string Name { get; }
        AttributeType AttrType { get; }
        object? Value { get; set; }
        event Action<object?, object?> OnValueChanged;

        void Dispose();
    }
}
