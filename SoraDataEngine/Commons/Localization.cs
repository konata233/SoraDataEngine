using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons
{
    /// <summary>
    /// 类库本地化
    /// </summary>
    internal class Localization
    {
        private static Dictionary<string, string>? LocalizedString { get; set; }

        public static void Init()
        {
            Init("en-us");
        }

        public static void Init(string designatedLanguage)
        {
            throw new NotImplementedException();
            // init the localization strings.
        }

        public static string? GetLocalization(string name)
        {
            string? value = null;
            if (LocalizedString != null)
            {
                LocalizedString.TryGetValue(name, out value);
            }
            return value;
        }

        public static string? GetLocalization(string name, out bool success)
        {
            string? value = string.Empty;
            if (LocalizedString != null)
            {
                success = LocalizedString.TryGetValue(name, out value);
            }
            else
            {
                success = false;
            }
            return value;
        }
    }
}
