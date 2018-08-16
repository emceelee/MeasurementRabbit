using System;
using System.Text;

using Newtonsoft.Json;

namespace Shared
{
    public static class MessagingUtility
    {
        public static byte[] SerializeObject(object o)
        {
            string jsonified = JsonConvert.SerializeObject(o);
            byte[] stream = Encoding.UTF8.GetBytes(jsonified);

            return stream;
        }
        public static T Deserialize<T>(byte[] stream)
        {
            string jsonified = Encoding.UTF8.GetString(stream);
            T obj = JsonConvert.DeserializeObject<T>(jsonified);

            return obj;
        }
    }
}
