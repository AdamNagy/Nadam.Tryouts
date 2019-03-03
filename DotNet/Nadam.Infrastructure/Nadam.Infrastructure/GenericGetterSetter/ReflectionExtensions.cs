using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Infrastructure.GenericGetterSetter
{
    public static class ReflectionExtensions
    {
        public static Expression<Func<object, object>> GetValueGetter(this PropertyInfo propertyInfo)
        {
            var getter = propertyInfo.GetGetMethod();
            return (obj) => getter.Invoke(obj, new object[] { });
        }

        public static Expression<Action<object, object>> GetValueSetter(this PropertyInfo propertyInfo)
        {
            var setter = propertyInfo.GetSetMethod();
            return (obj, val) => setter.Invoke(obj, new[] { val });
        }

        public static object GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
