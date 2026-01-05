// Services/MediaPipelineService.cs
using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Services.Filmhub;

public class MediaPipelineService
{
    private readonly Route4DbContext _context;
    private readonly IFilmhubService _filmhubService;

    public MediaPipelineService(Route4DbContext context, IFilmhubService filmhubService)
    {
        _context = context;
        _filmhubService = filmhubService;
    }

    public async Task TriggerFinalDistributionIfEligibleAsync(
        string clientSlug, 
        string releaseKey)
    {
        var release = await _context.ReleaseInstances
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => 
                r.Client!.Slug == clientSlug && 
                r.Key == releaseKey &&
                r.Status == "Archived");
        
        if (release == null) return;
        
        // Check conditions
        var witnessCount = await _context.WitnessEvents
            .CountAsync(w => w.ReleaseInstanceId == release.Id);
        
        // Note: Payment model structure may vary - adjust accordingly
        var revenue = 0m; // Placeholder - integrate with actual payment tracking
        
        if (witnessCount >= 100 && revenue >= 1000) // Configurable thresholds
        {
            await _filmhubService.SubmitForDistributionAsync(
                title: release.Title,
                description: release.Description ?? "",
                videoUrl: "", // From Frame.io storage
                posterUrl: $"assets/clients/{clientSlug}/cover.jpg",
                metadata: new
                {
                    createdVia = "Route4",
                    witnessCount,
                    revenue,
                    releaseDate = release.CompletedAt
                }
            );
        }
    }}