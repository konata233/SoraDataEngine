using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Attributes
{
    public enum AttributeType
    {
        None = 0,
        Numeric = 1,
        Boolean = 2,
        String = 3,
        NumericArray = 4,
        BooleanArray = 5,
        StringArray = 6,
        Enum = 7,
        Ptr = 8,
        Any = 9,
    }
}
