using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    class SiteCrackerJsonModel
    {
        public IEnumerable<SiteCrackerJsonStruct> SiteCrackerConfig { get; set; }

        public SiteCrackerJsonModel(/* file path for the json file that containd the configs */)
        {
            SiteCracker func = CreateFunction("wer", "sdf");
            func += CreateFunction("other", "some other");
        }

        public SiteCracker CreateFunction(string replaceWhat, string replaceWith)
        {
            return (string input) => input.Replace(replaceWhat, replaceWhat);
        }
    }

    public struct SiteCrackerJsonStruct
    {
        public string domain;
        public string replaceWhat;
        public string replaceWith;
    }
}
