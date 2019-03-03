using Nadam.Infrastructure.GenericGetterSetter;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nadam.Infrastructure
{
    public abstract class Entity
    {
        private Type Type { get; set; }
        private Dictionary<string, Action<object, object>> Setters { get; set; }
        private Dictionary<string, Func<object, object>> Getters { get; set; }
        private Dictionary<string, PropertyInfo> Properties { get; set; }

        private object child;

        protected void Init(object _child)
        {
            child = _child;
            Type = _child.GetType();
            Setters = new Dictionary<string, Action<object, object>>();
            Getters = new Dictionary<string, Func<object, object>>();
            var props = Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Properties = new Dictionary<string, PropertyInfo>();
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

        public void Update(string key, object value)
        {
            if (child == null)
                return;

            Action<object, object> setter;

            if (!Setters.TryGetValue(key, out setter))
                return;

            setter(child, value);
        }
    }
}
