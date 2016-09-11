using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib;

namespace TTSSX
{
    public class AutoCompleteStop
    {
        public string Name { get; set; }
        public List<Stop> Stops { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public static implicit operator string(AutoCompleteStop acs)
        {
            if(acs == null)
            {
                return null;
            }
            else
            {
                return acs.Name;
            }
        }
    }
}
