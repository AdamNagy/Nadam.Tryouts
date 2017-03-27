using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static Nadam.Lib.BinaryPredicates;

namespace Nadam.Lib
{
    /// <summary>
    /// This class contains extension methods for Reflection, Filters and other types methods like
    /// Foreach, PluralizeString...
    /// </summary>
    public static class Extensions
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

        /// <summary>
        /// Set the given property to null
        /// </summary>
        /// <typeparam name="T">the type of the object</typeparam>
        /// <param name="src">the object that you want to set one if its properties to null</param>
        /// <param name="property">the name of the property</param>
        /// <returns></returns>
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

        /// <summary>
        /// Same as the SetValueToNullFor method, just this one takes a list of propeti name that will made null
        /// </summary>
        /// <typeparam name="T">check SetValueToNullFor method</typeparam>
        /// <param name="src">check SetValueToNullFor method</param>
        /// <param name="properties">check SetValueToNullFor method</param>
        /// <returns></returns>
        public static T SetValuesToNullFor<T>(this T src, IEnumerable<string> properties)
        {
            foreach (var prop in properties)
            {
                var propertyInfo = src.GetType().GetProperty(prop);
                src.SetValueToNullFor(propertyInfo.Name);
            }
            return src;
        }

        /// <summary>
        /// Check the given property if exist for the given object
        /// </summary>
        /// <typeparam name="T">the type of the objet</typeparam>
        /// <param name="src">the object it selt you want tocheck</param>
        /// <param name="property">name of the property to check</param>
        /// <returns></returns>
        public static bool HasProperty<T>(this T src, string property)
        {
            return src.GetType().GetProperties().Select(p => p.Name).Contains(property);
        }

        /// <summary>
        /// Makes all the virtual properties null for the given object
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IEnumerable<object> MakeVirtualPropertiesNull(this IEnumerable<object> root)
        {
            if (root != null && root.Any())
            {
                //var domainVirtualProperties = root
                //    .First()
                //    .GetType()
                //    .GetProperties()
                //    .Where(p => p.GetMethod.IsVirtual);

                //root.Foreach(p =>
                //{
                //    p.SetValuesToNullFor(domainVirtualProperties.Select(q => q.Name));
                //});
                var virtualProperties = root
                                    .First()
                                    .GetVirtualPropertiesOf();
                
                root.Foreach(p => p.SetValuesToNullFor(virtualProperties));

            }
            return root;
        }

        public static object MakeVirtualPropertiesNull(this object root)
        {
            if (root != null)
            {        
                root.SetValuesToNullFor(root.GetVirtualPropertiesOf());               
            }
            return root;
        }

        public static IEnumerable<string> GetVirtualPropertiesOf(this object subject)
        {
            return subject
                    .GetType()
                    .GetProperties()
                    .Where(p => p.GetMethod.IsVirtual)
                    .Select(p => p.Name);
        }
        #endregion
        
        #region Filters
        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> domain, 
                                                 string filter, 
                                                 object reference, 
                                                 Func<object, object, bool> binaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (filter == "NoFilter")
                return domain;

            if (domain.First().HasProperty(filter))
            {
                return domain.Where(p => binaryPred(p.GetValueFor(filter), reference)).ToList();
            }
            throw new ArgumentException("Filterable property does not exist on domain object.");
        }

        public static IEnumerable<T> FilterBy<T, U>(this IEnumerable<T> domain, 
                                                    Func<T, U> propertySelector, 
                                                    Func<object, bool> unaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;
 
            return domain.Where(p => unaryPred(propertySelector(p))).ToList();
        }

        public static IEnumerable<T> FilterBy<T, U>(this IEnumerable<T> domain, 
                                                    Func<T, U> propertySelector, 
                                                    object reference, 
                                                    Func<object, object, bool> binaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;
                        
            return domain.Where(p => binaryPred(propertySelector(p), reference)).ToList();
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

        public static void Foreach<T>(this IEnumerable<T> list, Func<T, T> action)
        {
            var array = list as List<T>;
            for (int i = 0; i < list.Count(); i++)
            {
                array[i] = action(array[i]);
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

    }
}
