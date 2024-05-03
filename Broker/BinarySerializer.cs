using Newtonsoft.Json;
using System.Text;

namespace Broker
{
    public class BinarySerializer
    {
        public TObject Deserialize<TObject>(byte[] binaryData)
        {
            var obj = Encoding.UTF8.GetString(binaryData);
            var result = JsonConvert.DeserializeObject<TObject>(obj);
            return result;
        }
        public byte[] Serialize(object obj)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
            return data;
        }
    }
}
