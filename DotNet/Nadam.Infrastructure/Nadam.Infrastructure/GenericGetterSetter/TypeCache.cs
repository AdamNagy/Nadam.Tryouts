using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nadam.Infrastructure.GenericGetterSetter
{
    /// <summary>
    /// It caches the property setters and getters for the PUBLIC PROPERTIES of a type
    /// Works only with public properties, NOT private, protected and variables
    /// </summary>
    public class TypeCache
    {
        private static ConcurrentDictionary<Type, TypeCache> _typeCacheDict = new ConcurrentDictionary<Type, TypeCache>();

        public static TypeCache Get(Type type)
        {
            TypeCache cache;
            if (!_typeCacheDict.TryGetValue(type, out cache))
            {
                cache = new TypeCache(type);
                _typeCacheDict[type] = cache;
            }
            return cache;
        }

        private TypeCache(Type type)
        {
            Type = type;
            Setters = new ConcurrentDictionary<string, Action<object, object>>();
            Getters = new ConcurrentDictionary<string, Func<object, object>>();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Properties = new ConcurrentDictionary<string, PropertyInfo>();
            foreach (var prop in props)
            {
                if (prop.CanRead)
                {
                    var objGetter = prop.GetValueGetter();
                    Getters[prop.Name] = objGetter.Compile();
                }
                if (prop.CanWrite)
                {
                    var objSetter = prop.GetValueSetter();
                    Setters[prop.Name] = objSetter.Compile();
                }
                Properties[prop.Name] = prop;
            }
        }

        public Type Type { get; private set; }
        public ConcurrentDictionary<string, Action<object, object>> Setters { get; private set; }
        public ConcurrentDictionary<string, Func<object, object>> Getters { get; private set; }
        public ConcurrentDictionary<string, PropertyInfo> Properties { get; private set; }
    }
}
