namespace Route4MoviePlug.Api.Services.Filmhub;

/// <summary>
/// Interface for Filmhub distribution service
/// </summary>
public interface IFilmhubService
{
    /// <summary>
    /// Submits a release for distribution to Filmhub's multi-platform network
    /// </summary>
    Task SubmitForDistributionAsync(
        string title,
        string description,
        string videoUrl,
        string posterUrl,
        object metadata);
}
