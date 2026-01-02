using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Data;

public class Route4DbContext : DbContext
{
    public Route4DbContext(DbContextOptions<Route4DbContext> options) : base(options)
    {
    }

    // Original models
    public DbSet<Client> Clients { get; set; }
    public DbSet<SplashPage> SplashPages { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<MembershipTier> MembershipTiers { get; set; }
    public DbSet<TierFeature> TierFeatures { get; set; }
    public DbSet<StripeAccount> StripeAccounts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // Route4 Architecture Models
    public DbSet<ReleaseCycleTemplate> ReleaseCycleTemplates { get; set; }
    public DbSet<ReleaseStageTemplate> ReleaseStageTemplates { get; set; }
    public DbSet<ReleaseInstance> ReleaseInstances { get; set; }
    public DbSet<ReleaseStageExecution> ReleaseStageExecutions { get; set; }
    public DbSet<ReleaseArtifact> ReleaseArtifacts { get; set; }
    public DbSet<WitnessEvent> WitnessEvents { get; set; }
    public DbSet<RitualMapping> RitualMappings { get; set; }
    public DbSet<ReleaseStateTransition> ReleaseStateTransitions { get; set; }
    public DbSet<DiscordConfiguration> DiscordConfigurations { get; set; }
    public DbSet<DiscordChannel> DiscordChannels { get; set; }
    public DbSet<DiscordRole> DiscordRoles { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Slug).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Slug).IsUnique();
        });

        modelBuilder.Entity<SplashPage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subtitle).IsRequired().HasMaxLength(300);
            
            entity.HasOne(e => e.Client)
                .WithMany(c => c.SplashPages)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Icon).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.SplashPage)
                .WithMany(s => s.Benefits)
                .HasForeignKey(e => e.SplashPageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MembershipTier>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.TagLine).HasMaxLength(200);
            entity.Property(e => e.PriceInterval).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TierFeature>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Text).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Tier)
                .WithMany(t => t.Features)
                .HasForeignKey(e => e.TierId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StripeAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StripeAccountId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.ApplicationFeePercent).HasPrecision(5, 2);
            entity.HasIndex(e => e.StripeAccountId).IsUnique();
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StripePaymentIntentId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CustomerEmail).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.StripePaymentIntentId).IsUnique();
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.NoAction); // Changed from Cascade to prevent cycle
            
            entity.HasOne(e => e.MembershipTier)
                .WithMany()
                .HasForeignKey(e => e.MembershipTierId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StripeInvoiceId).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.PlatformFee).HasPrecision(18, 2);
            entity.Property(e => e.ClientAmount).HasPrecision(18, 2);
            entity.HasIndex(e => e.StripeInvoiceId).IsUnique();
            
            entity.HasOne(e => e.Payment)
                .WithMany()
                .HasForeignKey(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Route4 Architecture Model Configurations
        ConfigureRoute4Models(modelBuilder);

        // Seed initial data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var makingOfMaryId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var splashPageId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                Id = makingOfMaryId,
                Name = "Making of MARY",
                Slug = "making-of-mary",
                Description = "Exclusive behind-the-scenes documentary film project",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        );

        modelBuilder.Entity<SplashPage>().HasData(
            new SplashPage
            {
                Id = splashPageId,
                ClientId = makingOfMaryId,
                Title = "Exclusive VIP Membership",
                Subtitle = "Go Behind the Scenes",
                Description = "Get unprecedented access to the entire film making process of MAKING OF MARY. Join our exclusive community and witness every step of the creative journey.",
                IsPublished = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Benefit>().HasData(
            new Benefit
            {
                Id = Guid.NewGuid(),
                SplashPageId = splashPageId,
                Icon = "🎬",
                Title = "Behind-the-Scenes Content",
                Description = "Exclusive footage from set, rehearsals, and production meetings",
                DisplayOrder = 1
            },
            new Benefit
            {
                Id = Guid.NewGuid(),
                SplashPageId = splashPageId,
                Icon = "📝",
                Title = "Script Development",
                Description = "Access to screenplay drafts, revisions, and director's notes",
                DisplayOrder = 2
            },
            new Benefit
            {
                Id = Guid.NewGuid(),
                SplashPageId = splashPageId,
                Icon = "🎥",
                Title = "Production Diaries",
                Description = "Weekly updates from cast and crew throughout filming",
                DisplayOrder = 3
            },
            new Benefit
            {
                Id = Guid.NewGuid(),
                SplashPageId = splashPageId,
                Icon = "💬",
                Title = "Interactive Community",
                Description = "Direct Q&A sessions with filmmakers and cast members",
                DisplayOrder = 4
            }
        );

        // Seed Membership Tiers
        var tier1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var tier2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var tier3Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

        modelBuilder.Entity<MembershipTier>().HasData(
            new MembershipTier
            {
                Id = tier1Id,
                ClientId = makingOfMaryId,
                Name = "Signal Discovery",
                Description = "Pay per signal witnessed. Discover anonymous pre-artifacts and fragments as they emerge without ongoing subscriptions.",
                Price = 7.50m,
                PriceInterval = "per-signal",
                TagLine = "Cross the threshold to witness",
                IsFeatured = false,
                DisplayOrder = 1,
                IsActive = true
            },
            new MembershipTier
            {
                Id = tier2Id,
                ClientId = makingOfMaryId,
                Name = "Ritual Participation",
                Description = "Pay per writing table, shot council, or process session attended. Witness the creative process without story outcomes.",
                Price = 20.00m,
                PriceInterval = "per-session",
                TagLine = "Witness process, not outcomes",
                IsFeatured = true,
                DisplayOrder = 2,
                IsActive = true
            },
            new MembershipTier
            {
                Id = tier3Id,
                ClientId = makingOfMaryId,
                Name = "Private Viewing",
                Description = "Pay per private viewing event attended. Exclusive offline-safe access in protected locations with verified witness status.",
                Price = 35.00m,
                PriceInterval = "per-viewing",
                TagLine = "Sacred witness moments",
                IsFeatured = false,
                DisplayOrder = 3,
                IsActive = true
            }
        );

        modelBuilder.Entity<TierFeature>().HasData(
            // Signal Discovery Features (Threshold-based)
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Anonymous signal drops as they emerge", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "BTS fragments without story context", IsHighlighted = false, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Tools, hands, rooms — no explanations", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Pay only when you choose to witness", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "No subscriptions, no ongoing fees", IsHighlighted = false, DisplayOrder = 5 },
            
            // Ritual Participation Features (Threshold-based)
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Writing table sessions (decisions, not outcomes)", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Shot council participation (process preview)", IsHighlighted = true, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Color/edit sessions without story reveals", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Reflection channels after ritual participation", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Pay per session attended, not time-based", IsHighlighted = false, DisplayOrder = 5 },
            
            // Private Viewing Features (Threshold-based)
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Private location screenings (QR-based access)", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Offline-safe viewing with verified witness status", IsHighlighted = false, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "No recording, no sharing — sacred witness moments", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Invitation-only events for proven participants", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Pay only when invited and you choose to attend", IsHighlighted = false, DisplayOrder = 5 }
        );

        // Seed Stripe Account (for testing)
        modelBuilder.Entity<StripeAccount>().HasData(
            new StripeAccount
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                StripeAccountId = "acct_test_MakingOfMary", // Test account ID
                IsActive = true,
                ApplicationFeePercent = 10.00m, // Route4 takes 10%
                ConnectedAt = DateTime.UtcNow
            }
        );

        // Seed Mary's Release Cycle Template
        var maryReleaseCycleId = Guid.NewGuid();
        modelBuilder.Entity<ReleaseCycleTemplate>().HasData(
            new ReleaseCycleTemplate
            {
                Id = maryReleaseCycleId,
                ClientId = makingOfMaryId,
                Name = "Mary Episode Release Cycle",
                Description = "Full ritual sequence for Making of Mary episodes",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed Mary's Ritual Mappings - Phase 4 Implementation
        modelBuilder.Entity<RitualMapping>().HasData(
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Signal",
                StageType = "Signal",
                Description = "Anonymous pre-artifact song drop",
                ExecutionOrder = 1,
                TargetChannelPurpose = "signal",
                VisibilityLevel = "L2",
                RequiredRoles = null,
                IsReadOnly = true,
                DefaultDurationHours = null,
                OpenTrigger = "Manual",
                CloseTrigger = "Manual",
                AutomaticallyUnlockChannel = true,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = "🔊 **THE SIGNAL** — A new artifact has dropped. No context. No explanation. Just presence.",
                ClosingMessage = null,
                IsAnonymous = true,
                CanBeSkipped = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Process",
                StageType = "Process",
                Description = "Witness-only process rooms (Writing Table, Shot Council, etc.)",
                ExecutionOrder = 2,
                TargetChannelPurpose = "process",
                VisibilityLevel = "L1",
                RequiredRoles = new[] { "CoreTeam", "Witness" },
                IsReadOnly = false,
                DefaultDurationHours = 3,
                OpenTrigger = "Manual",
                CloseTrigger = "DurationExpired",
                AutomaticallyUnlockChannel = true,
                AutomaticallyLockChannel = true,
                SlowModeSeconds = "10",
                DisableFileUploads = true,
                DisableExternalEmojis = false,
                AnnouncementMessage = "⚒️ **PROCESS OPEN** — The room is now open. Decisions, not outcomes. Craft, not results.",
                ClosingMessage = "✅ Process session complete. The room is now closed.",
                IsAnonymous = false,
                CanBeSkipped = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Hold",
                StageType = "Hold",
                Description = "Intentional silence before the drop",
                ExecutionOrder = 3,
                TargetChannelPurpose = "signal",
                VisibilityLevel = "L2",
                RequiredRoles = null,
                IsReadOnly = true,
                DefaultDurationHours = 24,
                OpenTrigger = "PreviousStageCompleted",
                CloseTrigger = "DurationExpired",
                AutomaticallyUnlockChannel = false,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = null,
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Drop",
                StageType = "Drop",
                Description = "Primary episode release",
                ExecutionOrder = 4,
                TargetChannelPurpose = "releases",
                VisibilityLevel = "L3",
                RequiredRoles = null,
                IsReadOnly = true,
                DefaultDurationHours = 24,
                OpenTrigger = "ScheduledTime",
                CloseTrigger = "DurationExpired",
                AutomaticallyUnlockChannel = true,
                AutomaticallyLockChannel = true,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = "📺 **THE DROP** — The episode is here. Read-only for 24 hours. Experience it first.",
                ClosingMessage = "The 24-hour window has closed. Reflection begins.",
                IsAnonymous = false,
                CanBeSkipped = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Echo",
                StageType = "Echo",
                Description = "Post-drop reflection (no spoilers, no theories)",
                ExecutionOrder = 5,
                TargetChannelPurpose = "reflection",
                VisibilityLevel = "L3",
                RequiredRoles = new[] { "Witness", "Member" },
                IsReadOnly = false,
                DefaultDurationHours = 72,
                OpenTrigger = "PreviousStageCompleted",
                CloseTrigger = "Manual",
                AutomaticallyUnlockChannel = true,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = "30",
                DisableFileUploads = true,
                DisableExternalEmojis = false,
                AnnouncementMessage = "💬 **ECHO** — Reflection is open. What did you notice? No spoilers. No theories. Only presence.",
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Fragments",
                StageType = "Fragments",
                Description = "BTS residue and artifacts",
                ExecutionOrder = 6,
                TargetChannelPurpose = "fragments",
                VisibilityLevel = "L2",
                RequiredRoles = new[] { "CoreTeam" },
                IsReadOnly = true,
                DefaultDurationHours = null,
                OpenTrigger = "Manual",
                CloseTrigger = null,
                AutomaticallyUnlockChannel = false,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = "🎬 **FRAGMENTS** — Behind-the-scenes residue. No context. Just artifacts.",
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Interval",
                StageType = "Interval",
                Description = "Podcast and meta reflection",
                ExecutionOrder = 7,
                TargetChannelPurpose = "interval",
                VisibilityLevel = "L3",
                RequiredRoles = null,
                IsReadOnly = true,
                DefaultDurationHours = null,
                OpenTrigger = "Manual",
                CloseTrigger = null,
                AutomaticallyUnlockChannel = true,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = "🎙️ **INTERVAL** — Meta reflection podcast is live.",
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "PrivateViewing",
                StageType = "PrivateViewing",
                Description = "Witness-only private screening",
                ExecutionOrder = 8,
                TargetChannelPurpose = "invitations",
                VisibilityLevel = "L1",
                RequiredRoles = new[] { "Witness" },
                IsReadOnly = true,
                DefaultDurationHours = null,
                OpenTrigger = "Manual",
                CloseTrigger = null,
                AutomaticallyUnlockChannel = false,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = "🎟️ **PRIVATE VIEWING** — Invitations for Witness-only screening have been sent.",
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RitualMapping
            {
                Id = Guid.NewGuid(),
                ClientId = makingOfMaryId,
                ReleaseCycleTemplateId = maryReleaseCycleId,
                RitualName = "Archive",
                StageType = "Archive",
                Description = "Permanent record and canon",
                ExecutionOrder = 9,
                TargetChannelPurpose = "releases",
                VisibilityLevel = "L3",
                RequiredRoles = null,
                IsReadOnly = true,
                DefaultDurationHours = null,
                OpenTrigger = "PreviousStageCompleted",
                CloseTrigger = null,
                AutomaticallyUnlockChannel = false,
                AutomaticallyLockChannel = false,
                SlowModeSeconds = null,
                DisableFileUploads = false,
                DisableExternalEmojis = false,
                AnnouncementMessage = null,
                ClosingMessage = null,
                IsAnonymous = false,
                CanBeSkipped = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }

    private void ConfigureRoute4Models(ModelBuilder modelBuilder)
    {
        // Release Cycle Template
        modelBuilder.Entity<ReleaseCycleTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Release Stage Template
        modelBuilder.Entity<ReleaseStageTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.Property(e => e.VisibilityLevel).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DiscordChannelTemplate).HasMaxLength(100);
            
            entity.HasOne(e => e.ReleaseCycleTemplate)
                .WithMany(t => t.Stages)
                .HasForeignKey(e => e.ReleaseCycleTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Release Instance
        modelBuilder.Entity<ReleaseInstance>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Key).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CurrentStage).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => new { e.ClientId, e.Key }).IsUnique();
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.ReleaseCycleTemplate)
                .WithMany()
                .HasForeignKey(e => e.ReleaseCycleTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Release Stage Execution
        modelBuilder.Entity<ReleaseStageExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StageName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            
            entity.HasOne(e => e.ReleaseInstance)
                .WithMany(r => r.StageExecutions)
                .HasForeignKey(e => e.ReleaseInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Release Artifact
        modelBuilder.Entity<ReleaseArtifact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Kind).IsRequired().HasMaxLength(100);
            entity.Property(e => e.VisibilityLevel).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Stage).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ContentType).HasMaxLength(100);
            entity.Property(e => e.StorageProvider).HasMaxLength(50);
            entity.Property(e => e.ProviderAssetId).HasMaxLength(200);
            entity.Property(e => e.PlaybackEmbedUrl).HasMaxLength(1000);
            entity.Property(e => e.DownloadUrl).HasMaxLength(1000);
            
            entity.HasOne(e => e.ReleaseInstance)
                .WithMany(r => r.Artifacts)
                .HasForeignKey(e => e.ReleaseInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Witness Event
        modelBuilder.Entity<WitnessEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Stage).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DiscordUserId).HasMaxLength(50);
            entity.Property(e => e.DiscordChannelId).HasMaxLength(50);
            entity.Property(e => e.Metadata).HasMaxLength(2000);
            
            entity.HasOne(e => e.ReleaseInstance)
                .WithMany(r => r.WitnessEvents)
                .HasForeignKey(e => e.ReleaseInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Discord Configuration
        modelBuilder.Entity<DiscordConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.GuildId).HasMaxLength(50);
            entity.Property(e => e.BotToken).HasMaxLength(500); // Encrypted
            entity.Property(e => e.LanguagePack).HasMaxLength(50);
            
            entity.HasOne(e => e.Client)
                .WithOne(c => c.DiscordConfiguration)
                .HasForeignKey<DiscordConfiguration>(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Discord Channel
        modelBuilder.Entity<DiscordChannel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ChannelId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Purpose).IsRequired().HasMaxLength(50);
            entity.Property(e => e.VisibilityLevel).IsRequired().HasMaxLength(10);
            entity.HasIndex(e => new { e.DiscordConfigurationId, e.ChannelId }).IsUnique();
            
            entity.HasOne(e => e.DiscordConfiguration)
                .WithMany(d => d.Channels)
                .HasForeignKey(e => e.DiscordConfigurationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Discord Role
        modelBuilder.Entity<DiscordRole>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RoleId).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.HasIndex(e => new { e.DiscordConfigurationId, e.RoleId }).IsUnique();
            
            entity.HasOne(e => e.DiscordConfiguration)
                .WithMany(d => d.Roles)
                .HasForeignKey(e => e.DiscordConfigurationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Ritual Mapping - Phase 4
        modelBuilder.Entity<RitualMapping>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RitualName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.StageType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TargetChannelPurpose).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TargetChannelId).HasMaxLength(50);
            entity.Property(e => e.VisibilityLevel).IsRequired().HasMaxLength(10);
            entity.Property(e => e.AnnouncementMessage).HasMaxLength(2000);
            entity.Property(e => e.ClosingMessage).HasMaxLength(2000);
            entity.Property(e => e.NotesForAdmin).HasMaxLength(1000);
            
            entity.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to prevent cascade cycle
            
            entity.HasOne(e => e.ReleaseCycleTemplate)
                .WithMany()
                .HasForeignKey(e => e.ReleaseCycleTemplateId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to prevent cascade cycle
            
            entity.HasIndex(e => new { e.ClientId, e.ReleaseCycleTemplateId, e.RitualName }).IsUnique();
        });

        // Release State Transition - Audit trail
        modelBuilder.Entity<ReleaseStateTransition>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FromStage).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ToStage).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TransitionReason).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DiscordChannelsOpened).HasMaxLength(2000);
            entity.Property(e => e.DiscordChannelsLocked).HasMaxLength(2000);
            entity.Property(e => e.AnnouncementSent).HasMaxLength(2000);
            entity.Property(e => e.Notes).HasMaxLength(500);
            
            entity.HasOne(e => e.ReleaseInstance)
                .WithMany()
                .HasForeignKey(e => e.ReleaseInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.ReleaseInstanceId);
            entity.HasIndex(e => e.OccurredAt);
        });
    }

}
