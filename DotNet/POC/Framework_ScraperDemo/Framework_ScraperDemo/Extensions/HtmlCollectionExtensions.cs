using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework_ScraperDemo.Extensions
{
    public static class HtmlCollectionExtensions
    {
        public static IEnumerable<HtmlNode> ToListOrEmpty(this HtmlNodeCollection htmlNodeCollection)
            => htmlNodeCollection != null ? htmlNodeCollection.ToList() : new List<HtmlNode>();
    }
}