using System;

namespace Voidwell.API.Models
{
    public class LastStoreUpdate : OriginatorData
    {
        public string StoreName { get; set; }
        public DateTime? LastUpdated { get; set; }
        public TimeSpan UpdateInterval { get; set; }
    }
}
