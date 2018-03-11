using System;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Global.Lib
{
    /// <summary>
    /// This class contains extension methods for Filtering
    /// </summary>
    public static partial class Extensions
    {
        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> domain,
                                                 string property,
                                                 object reference,
                                                 Func<object, object, bool> binaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (property == "NoFilter")
                return domain;

            if (domain.First().HasProperty(property))
            {
                return domain.Where(p => binaryPred(p.GetValueFor(property), reference)).ToList();
            }
            throw new ArgumentException("Filterable property does not exist on domain object.");
        }

        public static IEnumerable<T> FilterBy<T, TU>(this IEnumerable<T> domain,
													Func<T, TU> property,
													object reference,
													Func<object, object, bool> binaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            return domain.Where(p => binaryPred(property(p), reference)).ToList();
        }

        public static IEnumerable<T> FilterBy<T>(this IEnumerable<T> domain,
                                                string property,
                                                Func<object, bool> unaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            if (property == "NoFilter")
                return domain;

            if (domain.First().HasProperty(property))
            {
                return domain.Where(p => unaryPred(p.GetValueFor(property))).ToList();
            }
            throw new ArgumentException("Filterable property does not exist on domain object.");
        }

        public static IEnumerable<T> FilterBy<T, TU>(this IEnumerable<T> domain,
                                                    Func<T, TU> property,
                                                    Func<object, bool> unaryPred)
        {
            domain = domain as IList<T> ?? domain.ToList();
            if (!domain.Any())
                return null;

            return domain.Where(p => unaryPred(property(p))).ToList();
        }
    }
}
