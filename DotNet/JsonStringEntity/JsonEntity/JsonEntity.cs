using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity
{
    public class JsonEntity
    {
        private JsonStringEntity jsonStringEntity;

        public string this[string propName]
        {
            get { return jsonStringEntity.Read(propName); }
        }
    }
}
