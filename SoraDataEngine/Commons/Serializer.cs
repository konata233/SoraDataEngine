using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons
{
    /// <summary>
    /// 序列化工具
    /// </summary>
    public class Serializer
    {
        public static byte[] SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            // note: Serialize() method is an unsafe and depricated method!
            return stream.ToArray();
        }

        public static object DeserializeObject(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            return formatter.Deserialize(stream);
        }

        public static T DeserializeObject<T>(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            return (T)formatter.Deserialize(stream);
        }
    }
}
