using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev
{
    public interface IEntryConfig
    {
        string Name { get; set; }
        string Description { get; set; }
        int StartLevel { get; set; }
        string[] Dependencies { get; set; }
    }
}
