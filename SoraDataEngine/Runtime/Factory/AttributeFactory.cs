using SoraDataEngine.Commons.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = SoraDataEngine.Commons.Attributes.Attribute;

namespace SoraDataEngine.Runtime.Factory
{
    /// <summary>
    /// 属性工厂
    /// </summary>
    public class AttributeFactory
    {
        public AttributeFactory() 
        { 
        }

        public static IAttribute MakeAttribute(string name, object value, AttributeType type = AttributeType.Any) 
        { 
            return new Attribute(name, value, type);
        }
    }
}
