using System.Runtime.Serialization;

namespace TTSSX
{
    public class PassagePostRq
    {
        [DataMember(Name = "theirId")]
        public string TheirId { get; set; }
        [DataMember(Name = "line")]
        public string Line { get; set; }
        [DataMember(Name = "plannedTime")]
        public string PlannedTime { get; set; }
        [DataMember(Name = "vehicleId")]
        public string VehicleId { get; set; }
        [DataMember(Name = "tramNo")]
        public string TramNo { get; set; }
        [DataMember(Name = "compNo")]
        public string CompositionNo { get; set; }
        [DataMember(Name = "tripId")]
        public string TripId { get; set; }
        [DataMember(Name = "routeId")]
        public string RouteId { get; set; }
        [DataMember(Name = "dir")]
        public string Direction { get; set; }
    }
}
