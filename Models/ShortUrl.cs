using System;

namespace Route4MoviePlug.Api.Models
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; } = null!;
        public string TargetUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }
}
