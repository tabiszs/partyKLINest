using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyKlinest.Infrastructure
{
    public class ExtensionPropertyNameBuilder
    {
        private string prefix;
        public ExtensionPropertyNameBuilder(string appId)
        {
            prefix = "extension_" + appId.Replace("-","") + "_";
        }

        public string GetExtensionName(string parameter) => prefix + parameter;
    }
}
