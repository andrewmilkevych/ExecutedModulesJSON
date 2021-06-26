using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutedModulesJSON.Core.Model
{
    class Module
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public string name { get; set; }
        public string parameter1 { get; set; }
        public string parameter2 { get; set; }
        public string parameter3 { get; set; }
    }
}
