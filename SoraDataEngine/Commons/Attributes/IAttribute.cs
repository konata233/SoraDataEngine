using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Attributes
{
    public interface IAttribute
    {
        string Name { get; }
        AttributeType AttrType { get; }
        object? Value { get; set; }
    }
}
