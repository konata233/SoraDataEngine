using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Attributes
{
    public class Attribute : IAttribute
    {
        public string Name { get; }
        public AttributeType AttrType { get; } = AttributeType.None;

        public object? Value { get; set; }

        public Attribute(string name, object value, AttributeType type)
        {
            Name = name;
            Value = value;
            AttrType = type;
        }

        public Attribute(string name, object value)
        {
            Name = name;
            Value = value;
            AttrType = AttributeType.None;
        }

        public override string ToString()
        {
            return string.Concat("Attribute ", Name, "Type:", AttrType.ToString() + "Value: ", Value?.ToString());
        }
    }
}
