using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitterBackUp.Services.Utils.Contracts
{
    public interface IJsonProvider
    {
        T DeserializeObject<T>(string jsonAsString);
        JObject ParseToJObject(string jsonAsString);
        JArray ParseToJArray(string jsonAsString);
        string SerializeObject(object someClass);
    }
}