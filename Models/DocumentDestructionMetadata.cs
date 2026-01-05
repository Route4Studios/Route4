using System;

namespace Route4MoviePlug.Api.Models
{
    public class DocumentDestructionMetadata
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentType { get; set; } = "";
        public string DestroyedBy { get; set; } = "";
        public DateTime DestroyedAt { get; set; }
        public string Hash { get; set; } = "";
        public string Reason { get; set; } = "";
    }
}
