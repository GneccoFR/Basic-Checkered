using Unity.Plastic.Newtonsoft.Json;

namespace Core.Networking
{
    public static class JsonHandler
    {
        //Todo: this may be change for a pure class with an interface
        public static string Serialize<T>(T data) => JsonConvert.SerializeObject(data);
        public static T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
    }
}
