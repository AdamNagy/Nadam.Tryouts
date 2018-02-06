using System;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.Lib
{
    /// <summary>
    /// This class contains other extension like forach, and some string extensions
    /// </summary>
    public static partial class Extensions
    {
        #region Base extensions
        //public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        //{
        //    foreach (var listItem in list)
        //    {
        //        action(listItem);
        //    }
        //}

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> list, Func<T, T> action)
        {
            var array = new List<T>();
	        foreach (var item in list)
	        {
				array.Add(action(item));
			}
	        return array;
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
