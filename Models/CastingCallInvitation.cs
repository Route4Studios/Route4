using System;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Models
{
    public class CastingCallInvitation
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Client? Client { get; set; }
        public Guid CastingCallId { get; set; }
        public CastingCall? CastingCall { get; set; }
        public string? InviteeName { get; set; }
        public string? InviteeEmail { get; set; }
        public string? Role { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRevoked { get; set; }
        public string? ArtifactUrl { get; set; }
        public string? Hash { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public string? ShortUrl { get; set; }
        public bool IsActive { get; set; }
        public string? PdfArtifactPath { get; set; }
    }
}
