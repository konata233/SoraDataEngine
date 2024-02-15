using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Attributes
{
    /// <summary>
    /// 基础属性类
    /// </summary>
    public class Attribute : IAttribute, IDisposable
    {
        public string Name { get; private set; }
        public AttributeType AttrType { get; set; } = AttributeType.None;

        private object? _value;

        public object? Value
        {
            get
            {
                return _value;
            }
            set
            {
                OnValueChanged?.Invoke(value, Value);
                _value = value;
            }
        }

        public event Action<object?, object?>? OnValueChanged;

        public Attribute(string name, object value, AttributeType type)
        {
            Name = name;
            _value = value;
            AttrType = type;
        }

        public Attribute(string name, object value)
        {
            Name = name;
            _value = value;
            AttrType = AttributeType.None;
        }

        public override string ToString()
        {
            return string.Concat("Attribute ", Name, "Type:", AttrType.ToString() + "Value: ", Value?.ToString());
        }

        public void Dispose()
        {
            
        }
    }
}
