using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Models.Data;

namespace TTSSX.Models
{
    public class PassageListItem
    {
        public PassageListItem()
        { }

        public PassageListItem(Passage passage)
        {
            Line = passage.Line;
            Direction = passage.Direction;
            TramDescription = passage.Vehicle != null ? $"{passage.Vehicle.SideNo} ({passage.Vehicle.ModelName})" : string.Empty;
            MixedTime = passage.MixedTime;
        }

        public string Line { get; set; }
        public string Direction { get; set; }
        public string TramDescription { get; set; }
        public string MixedTime { get; set; }
    }
}
