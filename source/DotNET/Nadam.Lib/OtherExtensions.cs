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
        public static void Each<T>(this IEnumerable<T> domainList, Func<T, T> action)
        {
	        var asList = domainList as IList<T>;
			if( asList == null || asList.Count == 0 )
				return;

	        for (int i = 0; i < asList.Count; i++)
		        asList[i] = action(asList[i]);
        }

        public static string PluralizeString(this string single)
        {
            if (string.IsNullOrEmpty(single))
                return string.Empty;

            if (single.Last() == 's')
                return single;

            return single + 's';
        }
    }
}
