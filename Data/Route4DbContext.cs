using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Models;

namespace Route4MoviePlug.Api.Data;

public class Route4DbContext : DbContext
{
    public Route4DbContext(DbContextOptions<Route4DbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<SplashPage> SplashPages { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<MembershipTier> MembershipTiers { get; set; }
    public DbSet<TierFeature> TierFeatures { get; set; }
    public DbSet<StripeAccount> StripeAccounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
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
                .OnDelete(DeleteBehavior.Cascade);
            
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
                Name = "Film Students",
                Description = "Perfect for aspiring filmmakers who want to learn from industry professionals and gain hands-on experience.",
                Price = 149.00m,
                PriceInterval = "year",
                TagLine = "Learn from the Voltron team",
                IsFeatured = false,
                DisplayOrder = 1,
                IsActive = true
            },
            new MembershipTier
            {
                Id = tier2Id,
                ClientId = makingOfMaryId,
                Name = "Investors",
                Description = "Become a co-producer and own a piece of the film. Get equity stake, executive producer credit, and VIP treatment at the premiere.",
                Price = 10000.00m,
                PriceInterval = "one-time",
                TagLine = "Own a piece of cinema history",
                IsFeatured = true,
                DisplayOrder = 2,
                IsActive = true
            },
            new MembershipTier
            {
                Id = tier3Id,
                ClientId = makingOfMaryId,
                Name = "Cult Fans",
                Description = "For true movie buffs who want the ultimate fan experience and exclusive access to the creative process.",
                Price = 179.00m,
                PriceInterval = "year",
                TagLine = "Join the inner circle",
                IsFeatured = false,
                DisplayOrder = 3,
                IsActive = true
            }
        );

        modelBuilder.Entity<TierFeature>().HasData(
            // Film Students Features
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Voltron masterclass series with industry pros", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "On-set access during filming", IsHighlighted = false, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Fox 45 studio tours", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Production diary & script access", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier1Id, Text = "Community Discord access", IsHighlighted = false, DisplayOrder = 5 },
            
            // Investors Features
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Equity stake in the film", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Executive Producer credit", IsHighlighted = true, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "VIP premiere & after-party access", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "Monthly investor updates", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier2Id, Text = "All Film Students + Cult Fans benefits", IsHighlighted = false, DisplayOrder = 5 },
            
            // Cult Fans Features
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Premiere party invitation", IsHighlighted = true, DisplayOrder = 1 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Exclusive merchandise & memorabilia", IsHighlighted = false, DisplayOrder = 2 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Cast & crew Q&A sessions", IsHighlighted = false, DisplayOrder = 3 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Behind-the-scenes video library", IsHighlighted = false, DisplayOrder = 4 },
            new TierFeature { Id = Guid.NewGuid(), TierId = tier3Id, Text = "Community Discord with special channels", IsHighlighted = false, DisplayOrder = 5 }
        );
    }
}
