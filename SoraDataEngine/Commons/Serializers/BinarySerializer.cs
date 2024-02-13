using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Serializers
{
    /// <summary>
    /// 序列化工具
    /// </summary>
    public class BinarySerializer
    {
        /// <summary>
        /// 序列化一个对象
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化后的字节</returns>
        public static byte[] SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            // note: Serialize() method is an unsafe and depricated method!
            return stream.ToArray();
        }

        /// <summary>
        /// 反序列化字节
        /// </summary>
        /// <param name="bytes">要反序列化的字节</param>
        /// <returns>一个对象</returns>
        public static object DeserializeObject(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            return formatter.Deserialize(stream);
        }

        /// <summary>
        /// 反序列化一个对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="bytes">要反序列化的字节</param>
        /// <returns>一个对象</returns>
        public static T DeserializeObject<T>(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            return (T)formatter.Deserialize(stream);
        }
    }
}
