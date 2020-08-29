using System;
using System.Collections.Generic;
using System.Reflection;

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
    }
}
