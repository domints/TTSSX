using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TTSSX
{
    public class TramGet
    {
        [DataMember(Name = "trams")]
        public string[] Trams { get; set; }
    }
}
