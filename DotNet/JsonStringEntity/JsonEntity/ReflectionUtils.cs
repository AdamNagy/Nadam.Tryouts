using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DataEntity
{
    public static class ReflectionUtils
    {
        public static Type GenerateGenericListTypeWithType(Type type)
        {
            MethodInfo createList_method = typeof(ReflectionUtils).GetMethod("CreateList",
                BindingFlags.Public | BindingFlags.Static);

            createList_method = createList_method.MakeGenericMethod(type);
            var typedList = createList_method.Invoke(null, null);

            return typedList.GetType();
        }

        public static List<T> CreateList<T>()
        {
            return new List<T>();
        }

        // maybe this is not the best place to have for this method
        public static MethodInfo Get_DeserializeObject_MethodInfo()
        {
            return typeof(JsonConvert).GetMethods()
                .Where(p => p.Name.Contains("DeserializeObject"))
                .Where(p => p.IsGenericMethod)
                .ToArray()[0];
        }
    }
}
