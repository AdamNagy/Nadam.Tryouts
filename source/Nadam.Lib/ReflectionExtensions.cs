using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nadam.Lib.ConsoleShell;

namespace Nadam.Lib
{
    /// <summary>
    /// This class contains extension methods for Reflection
    /// </summary>
    public static partial class Extensions
    {
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

        public static Type InnerType(this IEnumerable<Object> domain)
        {
            Type type = domain.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

		#region Attribute extensions
	    public static bool HasIgnoreAsCommandAttribute(this MethodInfo method)
	    {
		    if (method.GetCustomAttributes(typeof(IgnoreAsCommandAttribute)).Any())
			    return true;

		    return false;
	    }

	    public static bool HasIgnoreAsCommandAttribute(this Type commansClass)
	    {
		    if (commansClass.GetCustomAttribute<IgnoreAsCommandAttribute>() != null)
			    return true;
		    return false;
	    }

	    public static string[] GetCommandAliasesFromAttribute(this MethodInfo method)
	    {
		    var f = method.GetCustomAttributes(typeof(CommandShellAttribute));
		    CommandShellAttribute t;
		    if (f.GetType().Name.Contains("CommandShellAttribute"))
		    {
			    t = (CommandShellAttribute)f.First();
			    return t.CommandAliases;
		    }

		    return new string[1];
	    }
		#endregion

	}
}
