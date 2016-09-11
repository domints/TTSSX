using System.Runtime.Serialization;

namespace TTSSX
{
    public class PassagePostRs
    {
        [DataMember(Name = "vNo")]
        public int VehicleInstances { get; set; }
        [DataMember(Name = "pNo")]
        public int PassageInstances { get; set; }
    }
}
