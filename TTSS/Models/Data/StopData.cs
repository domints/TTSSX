using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Models.Enums;

namespace TTSSLib.Models.Data
{
    public class StopData : StopBase
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public StopType Type { get; set; }

        public override string ToString()
        {
            return base.ToString() + $":{Latitude}:{Longitude}:{Type}";
        }
    }
}
