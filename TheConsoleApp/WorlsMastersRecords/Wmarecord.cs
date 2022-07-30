using System;
using System.Collections.Generic;

namespace EFReflectionTableCopy.WorldMastersRecords
{
    public partial class Wmarecord
    {
        public string? Event { get; set; }
        public int? DistanceM { get; set; }
        public string? EventType { get; set; }
        public string? TrackType { get; set; }
        public string? Gender { get; set; }
        public short? Age { get; set; }
        public string? Result { get; set; }
        public float? Wind { get; set; }
    }
}
