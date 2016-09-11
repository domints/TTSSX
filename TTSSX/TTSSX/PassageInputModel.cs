using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSSX
{
    public class PassageInputModel
    {
        public PassageInputModel(string tram, string composition)
        {
            this.TramNo = tram;
            this.CompositionNo = composition;
        }

        public string TramNo { get; set; }
        public string CompositionNo { get; set; }
    }
}
