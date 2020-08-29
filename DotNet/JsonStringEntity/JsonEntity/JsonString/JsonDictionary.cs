using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataEntity
{
    public class JsonDictionary : IEnumerable<(string key, string value)>
    {
        private IEnumerable<(string key, string value)> _dict;

        public string this[string key]
        {
            get { return _dict.FirstOrDefault(p => p.key == key).value; }
        }

        public JsonDictionary(IEnumerable<(string key, string value)> dict)
        {
            _dict = dict;
        }

        public IEnumerator<(string key, string value)> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static T ToObject<T>(JsonDictionary jsonDictionary)
        {
            T domain = (T)Activator.CreateInstance(typeof(T));
            foreach (var property in typeof(T).GetProperties())
            {
                var loweredPropName = FirstLetterToLower(property.Name);

                if (jsonDictionary[loweredPropName] == null )
                    continue;

                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(domain, jsonDictionary[loweredPropName]);
                }
                else if (property.PropertyType.IsPrimitive)
                {
                    property.SetValue(domain, Convert.ToInt32(jsonDictionary[loweredPropName]));
                }
                else if (jsonDictionary[loweredPropName].StartsWith("{"))
                {
                    var deserializeObject_method = Get_DeserializeObject_MethodInfo();

                    deserializeObject_method = deserializeObject_method.MakeGenericMethod(property.PropertyType);
                    // The "null" is because it's a static method
                    var propVal = deserializeObject_method.Invoke(null, new[] { jsonDictionary[loweredPropName] });
                    property.SetValue(domain, propVal);
                }
                else if (jsonDictionary[loweredPropName].StartsWith("[")
                         && property.PropertyType.IsGenericType
                         && property.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    Type innerTypeOfArray = property.PropertyType.GetGenericArguments()[0];

                    var deserializeObject_method = Get_DeserializeObject_MethodInfo();

                    deserializeObject_method = deserializeObject_method.MakeGenericMethod(ReflectionUtils.GenerateGenericListTypeWithType(innerTypeOfArray));
                    // The "null" is because it's a static method
                    var propVal = deserializeObject_method.Invoke(null, new [] { jsonDictionary[loweredPropName] });
                    property.SetValue(domain, propVal);
                }
            }

            return domain;
        }

        public static MethodInfo Get_DeserializeObject_MethodInfo()
        {
            return typeof(JsonConvert).GetMethods()
                .Where(p => p.Name.Contains("DeserializeObject"))
                .Where(p => p.IsGenericMethod)
                .ToArray()[0];
        }

        public static string FirstLetterToLower(string text)
        {
            if (text != string.Empty && char.IsUpper(text[0]))
                return $"{char.ToLower(text[0])}{text.Substring(1)}";

            return text;
        }
    }
}
