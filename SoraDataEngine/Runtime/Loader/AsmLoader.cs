using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Composition;
using SoraDataEngine.Commons.Scopes;

namespace SoraDataEngine.Runtime.Loader
{
    internal class AsmLoader
    {
        [ImportMany]
        public IEnumerable<IScope> Scopes { get; set; }

        public AsmLoader() 
        {
            
        }
    }
}
