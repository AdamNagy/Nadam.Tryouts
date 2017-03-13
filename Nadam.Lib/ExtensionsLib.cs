using System;
using System.Collections.Generic;
using System.Linq;
using static Nadam.Lib.PredicatesLib;

namespace Nadam.Lib
{
    /// <summary>
    /// This class contains extension methods for Reflection, Filters and other types methods like
    /// Foreach, PluralizeString...
    /// </summary>
    public static class ExtensionsLib
    {
        #region Reflection extensions
        /// <summary>
        /// Gets the value of a specific property for an object
        /// </summary>
        /// <typeparam name="T">T type parameter is the type of the object yout want to get value for</typeparam>
        /// <param name="src">src is the object it self (stands for source), type T</param>
        /// <param name="property">peroperty is the name of the property you want to get the value, type string</param>
        /// <returns></returns>
        public static object GetValueFor<T>(this T src, string property)
        {
            return src.GetType().GetProperty(property).GetValue(src, null);
        }

        /// <summary>
        /// Sets the value of a sepcific property for an object
        /// </summary>
        /// <typeparam name="T">T type parameter is the type of the object to set value</typeparam>
        /// <param name="src">src(source) is the opbject you want to set a property</param>
        /// <param name="property">name of the property</param>
        /// <param name="val">value of the property</param>
        /// <returns></returns>
        public static object SetValueFor<T>(this T src, string property, object val)
        {
            var propertyInfo = src.GetType().GetProperty(property);
            propertyInfo.SetValue(src, Convert.ChangeType(val, propertyInfo.PropertyType), null);
            return src;
        }

        
        public static object SetValueToNullFor<T>(this T src, string property)
        {
            var type = src.GetType();
            var propertyInfo = type.GetProperty(property);
            var propType = propertyInfo.PropertyType;
            if (propType.IsGenericType
                //&& propType.GetGenericTypeDefinition() == typeof(Nullable<>)
                || propType.IsArray)
            {
                propertyInfo.SetValue(src, null);
            }
            else if (propType == typeof(String))
            {
                propertyInfo.SetValue(src, String.Empty);
            }
            else
            {
                try
                {
                    //propertyInfo.SetValue(src, null);
                    propertyInfo.SetValue(src, Activator.CreateInstance(propType));
                }
                catch (Exception ex)
                {
                    propertyInfo.SetValue(src, null);
                }
            }


            return src;
        }

        public static T SetValuesToNullFor<T>(this T src, IEnumerable<string> properties)
        {
            foreach (var prop in properties)
            {
                var propertyInfo = src.GetType().GetProperty(prop);
                src.SetValueToNullFor(propertyInfo.Name);
            }
            return src;
        }

        public static bool HasProperty<T>(this T src, string property)
        {
            return src.GetType().GetProperties().Select(p => p.Name).Contains(property);
        }

        public static IEnumerable<object> MakeVirtualPropertiesNull(this IEnumerable<object> root)
        {
            if (root != null && root.Any())
            {
                var domainVirtualProperties = root
                    .First()
                    .GetType()
                    .GetProperties()
                    .Where(p => p.GetMethod.IsVirtual);

                root.Foreach(p =>
                {
                    p.SetValuesToNullFor(domainVirtualProperties.Select(q => q.Name));
                });
            }
            return root;
        }
        #endregion


        #region Filters
        public static IEnumerable<T> FilterByEquality<T>(this IEnumerable<T> domain, string filter, object reference)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (domain.First().HasProperty(filter))
            {
                return domain.FilterBy(filter, reference, PredicatesLib.EqualityPredicate);
            }

            throw new ArgumentException("Filterable property does not exist on domain object.");
        }

        public static IEnumerable<T> FilterByGreaterThan<T>(this IEnumerable<T> domain, string filter, string reference)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (domain.First().HasProperty(filter))
            {
                return domain.FilterBy<T>(filter, reference, PredicatesLib.GreaterThanPredicate);
            }

            throw new ArgumentException("Filterable property does not exist on domain object.");
        }

        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> domain, string filter, object reference, Func<object, object, bool> pred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (filter == "NoFilter")
                return domain;

            if (domain.First().HasProperty(filter))
            {
                return domain.Where(p => pred(p.GetValueFor(filter), reference)).ToList();
            }
            throw new ArgumentException("Filterable property does not exist on domain object.");
        }
        #endregion


        #region Base extensions
        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var listItem in list)
            {
                action(listItem);
            }
        }

        public static string PluralizeString(this string single)
        {
            if (string.IsNullOrEmpty(single))
                return string.Empty;

            if (single.Last() == 's')
                return single;
            return single + 's';
        }
        #endregion

        public static Func<object, object, bool> SingleOrDefaultPredicate(this string funcName)
        {
            switch(funcName)
            {
                case "EqualityPredicate":
                case "Equality":
                    return EqualityPredicate;
                case "GreaterThanPredicate":
                case "GreaterThan":
                case "Greater":
                    return GreaterThanPredicate;
                case "LessThanPredicate":
                case "LessThan":
                case "Less":
                    return LessThanPredicate;
                default:
                    return NoFilter;
            }
        }
    }
}
