using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Loader
{
    public class AsmLoaderConfig
    {
        public string AsmLoadPath { get; set; }
        public string AsmSearchPattern { get; set; }

        public AsmLoaderConfig(string loadPath, string pattern = "*.dll") 
        { 
            AsmLoadPath = loadPath;
            AsmSearchPattern = pattern;
        }
    }
}
