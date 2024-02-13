using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SoraDataEngine.Commons.Serializers
{
    public class JsonDataSerializer
    {
        public static byte[] SerializeToUTF8Bytes<T>(T @object)
        {
            byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(@object);
            return bytes;
        }

        public static string SerializeToString<T>(T @object)
        {
            return JsonSerializer.Serialize(@object);
        }

        public static T? Deserialize<T>(string @string) 
        {
            return JsonSerializer.Deserialize<T>(@string);
        }
    }
}
