namespace Route4MoviePlug.Api.Services.Discord;

public class DiscordSettings
{
    public required string BotToken { get; set; }
    public required string ClientId { get; set; }
    public string? GuildId { get; set; }
    public bool UseMockService { get; set; } = true;
    public string DefaultGuildName { get; set; } = "Route4 Server";
    public bool EnableLogging { get; set; } = false;
}