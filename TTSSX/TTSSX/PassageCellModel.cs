using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib;

namespace TTSSX
{
    public class PassageCellModel
    {
        public string Line { get; set; }
        public string Direction { get; set; }
        public string Time { get; set; }
        public string TramDesc { get; set; }
        public bool Old { get; set; }
        public int RelativeTime { get; set; }
        public StopPassage Passage { get; set; }
    }
}
