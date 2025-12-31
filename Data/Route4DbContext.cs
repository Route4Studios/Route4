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
    }
}
