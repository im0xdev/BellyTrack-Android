using System;

namespace BellyTrack.Core.Models
{
    public class BellyEntryModel
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid IdentifyGuid { get; set; }
    }
}