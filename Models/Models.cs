namespace Route4MoviePlug.Api.Models;

public class Client
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public ICollection<SplashPage> SplashPages { get; set; } = new List<SplashPage>();
}

public class SplashPage
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
}

public class Benefit
{
    public Guid Id { get; set; }
    public Guid SplashPageId { get; set; }
    public SplashPage? SplashPage { get; set; }
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int DisplayOrder { get; set; }
}

public class SplashPageDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public string? HeroImageUrl { get; set; }
    public string? LogoUrl { get; set; }
    public List<BenefitDto> Benefits { get; set; } = new();
}

public class BenefitDto
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}

public class CreateSplashPageRequest
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public string? Description { get; set; }
    public List<CreateBenefitRequest> Benefits { get; set; } = new();
}

public class CreateBenefitRequest
{
    public required string Icon { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}
