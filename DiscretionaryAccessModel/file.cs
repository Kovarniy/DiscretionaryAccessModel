using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DiscretionaryAccessModel
{
    class File
    {
        public string text { get; set; }

        public Dictionary<string, int> access { get; set; }

    }

}
