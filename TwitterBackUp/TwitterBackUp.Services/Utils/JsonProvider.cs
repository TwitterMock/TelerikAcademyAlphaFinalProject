using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TwitterBackUp.Services.Services.Contracts;
using TwitterBackUp.Services.Utils.Contracts;

namespace TwitterBackUp.Services.Utils
{
    public class JsonProvider : IJsonProvider
    {
        public T DeserializeObject<T>(string jsonAsString)
        {
            return JsonConvert.DeserializeObject<T>(jsonAsString);
        }

        public JObject ParseToJObject(string jsonAsString)
        {
            return JObject.Parse(jsonAsString);
        }

        public JArray ParseToJArray(string jsonAsString)
        {
            return JArray.Parse(jsonAsString);
        }
        public string SerializeObject(object someClass) {
            return JsonConvert.SerializeObject(someClass);
        }
    }
}