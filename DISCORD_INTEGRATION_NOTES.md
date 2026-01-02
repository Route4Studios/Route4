# Discord.NET Integration - Lessons Learned

## Critical Fixes Applied

### 1. **Persistent DiscordSocketClient Required**
**Problem:** Creating new `DiscordSocketClient` instance for each API request caused connection timeouts.  
**Solution:** Maintain singleton client with connection reuse:
```csharp
private DiscordSocketClient? _client;
private readonly SemaphoreSlim _connectionLock = new(1, 1);
private bool _isConnected = false;

// Reuse existing connected client
if (_client != null && _isConnected && _client.ConnectionState == ConnectionState.Connected)
    return _client;
```

### 2. **Gateway Intents Must Match Bot Permissions**
**Problem:** Using `GatewayIntents.Guilds | GatewayIntents.GuildMembers` caused 4014 error "Disallowed intent(s)"  
**Solution:** Only request Guilds intent (GuildMembers requires privileged access approval):
```csharp
var config = new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.Guilds // NOT GuildMembers
};
```

### 3. **Bot Token Must Be Valid**
**Problem:** 401 Unauthorized errors when token expired  
**Solution:** Regenerate bot token from Discord Developer Portal → Bot → Reset Token  
**Location:** `appsettings.Development.json` → `DiscordSettings:BotToken`

### 4. **Rate Limiting for Channel/Role Creation**
**Problem:** Discord API rate limits can cause failures  
**Solution:** Add 500ms delays between bulk operations:
```csharp
await guild.CreateTextChannelAsync(name);
await Task.Delay(500); // Rate limit protection
```

### 5. **Idempotent Operations Required**
**Problem:** Re-running setup creates duplicate channels/roles  
**Solution:** Check for existing resources before creating:
```csharp
var existingChannel = guild.TextChannels.FirstOrDefault(c => c.Name == channelName);
if (existingChannel != null) return existingChannel.Id.ToString();
```

## Architecture Decisions

### Why SQL Server Instead of In-Memory?
- Release state machine requires persistent audit trail
- Ritual mappings need to survive restarts
- Production-ready data layer for multi-tenant deployments

### Why Persistent Discord Client?
- Discord.NET Ready event is asynchronous (takes ~2-5 seconds)
- Creating client per request means Ready never fires before request timeout
- Singleton pattern with semaphore ensures thread-safe connection reuse

### Why Skip GuildMembers Intent?
- Only needed for member presence/user cache
- Requires privileged intent approval from Discord
- Channel/role management only needs Guilds intent

## Current Limitations

### TODO: Channel Locking/Unlocking
```csharp
// Needs permission overwrite implementation:
await channel.AddPermissionOverwriteAsync(role, new OverwritePermissions(viewChannel: PermValue.Deny));
```

### TODO: Slow Mode
```csharp
// Needs ITextChannel.SlowModeInterval property:
await channel.ModifyAsync(ch => ch.SlowModeInterval = slowModeSeconds);
```

### TODO: Message Sending
```csharp
// Needs IMessageChannel.SendMessageAsync:
await channel.SendMessageAsync(message);
```

## Testing Checklist

- [x] SQL Server connection (localhost:1433)
- [x] Database seed data (9 RitualMappings for Making of MARY)
- [x] Discord bot connects successfully
- [x] 9 channels created (signal, process, hold, drop, echo, fragments, interval, private-viewing, archive)
- [x] 3 roles created (INNER CIRCLE, WITNESSED, FRAGMENTS)
- [x] Setup status endpoint returns correct step progression
- [x] Frontend displays bot invitation URL
- [ ] Channel locking/unlocking (permission overwrites)
- [ ] Slow mode setting
- [ ] Message sending with announcements
- [ ] End-to-end release cycle advancement
- [ ] Discord automation triggers on state transitions

## Next Steps

1. **Implement Permission Overwrites:**
   - LockChannelAsync: Deny view for @everyone or specific roles
   - UnlockChannelAsync: Remove deny overwrites

2. **Test Release Cycle:**
   - Create S1E1 release instance
   - Advance through 9 stages
   - Verify Discord automation at each stage

3. **Build Release Management UI:**
   - Release list/detail components
   - State machine visualization
   - History timeline

4. **Production Readiness:**
   - EF Core migrations (replace EnsureCreated)
   - Error handling for Discord API failures
   - Retry logic for transient failures
   - Monitoring/logging for Discord operations
