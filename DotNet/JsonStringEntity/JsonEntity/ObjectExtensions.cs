using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataEntity
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this Object subject)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            };

            return JsonConvert.SerializeObject(subject, jsonSerializerSettings);
        }
    }
}
