using System;

namespace Nadam.Infrastructure.GenericGetterSetter
{
    public static class TypeCacheExtensions
    {
        /// <summary>
        /// Set the value of a property by name
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this TypeCache cache, object item, string key, T value)
        {
            if (cache == null || item == null) return;
            Action<object, object> setter;
            if (!cache.Setters.TryGetValue(key, out setter)) return;
            setter(item, (object)value);
        }

        /// <summary>
        /// Get the value of a property by name
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="item"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this TypeCache cache, object item, string key)
        {
            if (cache == null || item == null) return default(T);
            Func<object, object> getter;
            if (!cache.Getters.TryGetValue(key, out getter)) return default(T);
            return (T)getter(item);
        }

        /// <summary>
        /// Set the value for a property to default by name
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="item"></param>
        /// <param name="key"></param>
        public static void Delete(this TypeCache cache, object item, string key)
        {
            if (cache == null || item == null) return;
            Action<object, object> setter;
            if (!cache.Setters.TryGetValue(key, out setter)) return;
            var value = cache.Properties[key].PropertyType.GetDefaultValue();
            setter(item, value);
        }

        /// <summary>
        /// Set the values for all the public properties of a class to their default
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="item"></param>
        public static void Clear(this TypeCache cache, object item)
        {
            if (cache == null || item == null) return;
            Action<object, object> setter;
            foreach (var pair in cache.Properties)
            {
                if (!cache.Setters.TryGetValue(pair.Key, out setter)) continue;
                var value = pair.Value.PropertyType.GetDefaultValue();
                setter(item, value);
            }
        }
    }
}
