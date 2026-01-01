namespace Route4MoviePlug.Api.Services.Discord;

/// <summary>
/// Route4 Default Discord Templates
/// As defined in the architecture document - neutral, reusable templates
/// </summary>
public static class Route4DiscordTemplates
{
    public static DiscordChannelTemplateSet GetDefaultChannelTemplates()
    {
        return new DiscordChannelTemplateSet
        {
            Orientation = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "signal",
                    Purpose = "signal",
                    VisibilityLevel = "L2",
                    IsReadOnly = true,
                    Description = "Pre-release artifacts and announcements"
                },
                new() {
                    Name = "how-to-witness",
                    Purpose = "orientation",
                    VisibilityLevel = "L2",
                    IsReadOnly = true,
                    Description = "Participation rules and expectations"
                },
                new() {
                    Name = "start-here",
                    Purpose = "orientation",
                    VisibilityLevel = "L2",
                    IsReadOnly = true,
                    Description = "Entry point for new members"
                }
            },
            
            Releases = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "releases",
                    Purpose = "releases",
                    VisibilityLevel = "L3",
                    IsReadOnly = true,
                    Description = "Primary release drops (read-only during first window)"
                },
                new() {
                    Name = "soundtrack",
                    Purpose = "soundtrack",
                    VisibilityLevel = "L3",
                    IsReadOnly = true,
                    Description = "Companion audio artifacts"
                }
            },
            
            Reflection = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "after-the-drop",
                    Purpose = "reflection",
                    VisibilityLevel = "L3",
                    IsReadOnly = false,
                    AllowedRoleTypes = new[] { "Witness", "Member" },
                    Description = "Opens after delay; reflection-only discussion"
                },
                new() {
                    Name = "interval",
                    Purpose = "interval",
                    VisibilityLevel = "L3",
                    IsReadOnly = true,
                    Description = "Podcast and meta reflection drops (read-only)"
                }
            },
            
            Residue = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "fragments",
                    Purpose = "fragments",
                    VisibilityLevel = "L2",
                    IsReadOnly = true,
                    AllowedRoleTypes = new[] { "CoreTeam" },
                    Description = "BTS residue and artifacts (post-only by approved roles)"
                }
            },
            
            Invitations = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "private-viewing",
                    Purpose = "invitations",
                    VisibilityLevel = "L1",
                    IsReadOnly = true,
                    AllowedRoleTypes = new[] { "Witness" },
                    Description = "Witness-only invitation notices"
                }
            },
            
            Process = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "writing-table",
                    Purpose = "process",
                    VisibilityLevel = "L1",
                    IsReadOnly = false,
                    IsProcessRoom = true,
                    AllowedRoleTypes = new[] { "CoreTeam", "Witness" },
                    Description = "Opened only during writing sessions"
                },
                new() {
                    Name = "shot-council",
                    Purpose = "process",
                    VisibilityLevel = "L1",
                    IsReadOnly = false,
                    IsProcessRoom = true,
                    AllowedRoleTypes = new[] { "CoreTeam", "Witness" },
                    Description = "Opened only during shot sessions"
                },
                new() {
                    Name = "cut-room",
                    Purpose = "process",
                    VisibilityLevel = "L1",
                    IsReadOnly = false,
                    IsProcessRoom = true,
                    AllowedRoleTypes = new[] { "CoreTeam", "Witness" },
                    Description = "Opened only during editing sessions"
                },
                new() {
                    Name = "color-before-meaning",
                    Purpose = "process",
                    VisibilityLevel = "L1",
                    IsReadOnly = false,
                    IsProcessRoom = true,
                    AllowedRoleTypes = new[] { "CoreTeam", "Witness" },
                    Description = "Opened only during color sessions"
                }
            },
            
            Private = new List<DiscordChannelTemplate>
            {
                new() {
                    Name = "core-team",
                    Purpose = "private",
                    VisibilityLevel = "L0",
                    IsReadOnly = false,
                    AllowedRoleTypes = new[] { "CoreTeam" },
                    Description = "Client team only"
                },
                new() {
                    Name = "canon-drafts",
                    Purpose = "private",
                    VisibilityLevel = "L0",
                    IsReadOnly = false,
                    AllowedRoleTypes = new[] { "CoreTeam" },
                    Description = "Story and canon development"
                }
            }
        };
    }
    
    public static DiscordRoleTemplateSet GetDefaultRoleTemplates()
    {
        return new DiscordRoleTemplateSet
        {
            CoreTeam = new DiscordRoleTemplate
            {
                Name = "Core Team",
                Type = "CoreTeam",
                Description = "Client creators and core team members",
                Color = 0x9147FF, // Purple
                IsHoisted = true,
                IsMentionable = false
            },
            
            Witness = new DiscordRoleTemplate
            {
                Name = "Witness",
                Type = "Witness",
                Description = "Trusted community members with process access",
                Color = 0x1ABC9C, // Teal
                IsHoisted = true,
                IsMentionable = false
            },
            
            Member = new DiscordRoleTemplate
            {
                Name = "Member",
                Type = "Member",
                Description = "Default community member",
                Color = null, // Default color
                IsHoisted = false,
                IsMentionable = false
            }
        };
    }
    
    /// <summary>
    /// Mary-specific language pack customizations
    /// Example of how client customization works while preserving template structure
    /// </summary>
    public static DiscordChannelTemplateSet GetMaryLanguagePack()
    {
        var templates = GetDefaultChannelTemplates();
        
        // Mary keeps most defaults but might customize specific terms
        // This shows how language packs work without breaking the architecture
        
        return templates;
    }
}

/// <summary>
/// Channel Template Extensions for Route4 Architecture
/// </summary>
public static class DiscordTemplateExtensions
{
    /// <summary>
    /// Apply client-specific language pack to templates
    /// </summary>
    public static DiscordChannelTemplateSet ApplyLanguagePack(
        this DiscordChannelTemplateSet templates, 
        Dictionary<string, string>? languageOverrides)
    {
        if (languageOverrides == null) return templates;
        
        // Apply language overrides while preserving structure and intent
        // This is where client customization happens without breaking Route4 constraints
        
        foreach (var channelList in new[] { 
            templates.Orientation, templates.Releases, templates.Reflection, 
            templates.Residue, templates.Invitations, templates.Process, templates.Private })
        {
            foreach (var channel in channelList)
            {
                if (languageOverrides.TryGetValue(channel.Name, out var customName))
                {
                    channel.Name = customName;
                }
            }
        }
        
        return templates;
    }
}