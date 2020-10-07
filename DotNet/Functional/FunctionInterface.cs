using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    /// <summary>
    /// the container that scans for the function interface
    /// </summary>
    public class SiteCrackerContainer
    {
        public IEnumerable<SiteCracker> SiteCrackers { get; set; }

        public SiteCrackerContainer()
        {
            foreach (var methodInfo in typeof(FunctionInterface).GetMethods(BindingFlags.Public | BindingFlags.Static))
            {
                var siteHandlerAttribute = methodInfo.GetCustomAttribute<SiteHandlerAttribute>();
            } 
        }
    }

    /// <summary>
    /// interface delegate
    /// </summary>
    /// <param name="input">just a test param</param>
    /// <returns></returns>
    public delegate string SiteCracker(string input);

    /// <summary>
    /// interface attribute
    /// </summary>
    public class SiteHandlerAttribute : Attribute
    {
        public Sites Handles { get; set; }

        public SiteHandlerAttribute(Sites handles)
        {
            Handles = handles;
        }
    }

    /// <summary>
    /// interface enum
    /// </summary>
    public enum Sites
    {
        SiteA, SiteB, SiteC
    }

    /// <summary>
    /// this static class that holds the functions
    /// </summary>
    public static class FunctionInterface
    {
        [SiteHandler(Sites.SiteA)]
        public static string HandleA(string input)
        {
            return $"site: A, {input}";
        }

        [SiteHandler(Sites.SiteB)]
        public static string HandleB(string input)
        {
            return $"site: B, {input}";
        }

        [SiteHandler(Sites.SiteB)]
        public static string HandleC(string input)
        {
            return $"site: C, {input}";
        }
    }
}
