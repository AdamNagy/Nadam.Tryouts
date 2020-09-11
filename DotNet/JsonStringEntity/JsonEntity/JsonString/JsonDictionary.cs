using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataEntity
{
    public class JsonDictionary : IEnumerable<(string key, string value)>
    {
        private IEnumerable<(string key, string value)> _dict;

        public string this[string key]
        {
            get { return _dict.FirstOrDefault(p => p.key == key).value; }
        }

        public bool ContainsProperty(string propName)
        {
            var val = _dict.FirstOrDefault(p => p.key == propName);
            return val.key != null;
        }

        public IEnumerable<string> Keys()
            => _dict.Select(p => p.key);

        public IEnumerable<string> Values()
            => _dict.Select(p => p.value);

        public JsonDictionary(IEnumerable<(string key, string value)> dict)
        {
            int outherIndex = 0, innerIndex = 0;
            foreach (var outherItem in dict)
            {
                foreach (var innerItem in dict)
                {
                    if (outherIndex != innerIndex && outherItem.key == innerItem.key)
                        throw new ArgumentException($"Json object contains property '{outherItem.key}' multiple times");

                    ++innerIndex;
                }

                ++outherIndex;
                innerIndex = 0;
            }
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
                var loweredPropName = StringUtils.FirstLetterToLower(property.Name);

                if (!jsonDictionary.ContainsProperty(loweredPropName))
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
                    var deserializeObject_method = ReflectionUtils.Get_DeserializeObject_MethodInfo();

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

                    var deserializeObject_method = ReflectionUtils.Get_DeserializeObject_MethodInfo();

                    deserializeObject_method = deserializeObject_method.MakeGenericMethod(ReflectionUtils.GenerateGenericListTypeWithType(innerTypeOfArray));
                    // The "null" is because it's a static method
                    var propVal = deserializeObject_method.Invoke(null, new [] { jsonDictionary[loweredPropName] });
                    property.SetValue(domain, propVal);
                }
            }

            return domain;
        }
    }
}
