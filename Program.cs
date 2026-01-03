using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Middleware;
using Route4MoviePlug.Api.Services;
using Route4MoviePlug.Api.Services.Discord;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add DbContext with SQL Server
builder.Services.AddDbContext<Route4DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Stripe configuration
var stripeSettings = new StripeSettings
{
    SecretKey = builder.Configuration["Stripe:SecretKey"] ?? "sk_test_YOUR_TEST_SECRET_KEY",
    PublishableKey = builder.Configuration["Stripe:PublishableKey"] ?? "pk_test_YOUR_TEST_PUBLISHABLE_KEY",
    WebhookSecret = builder.Configuration["Stripe:WebhookSecret"] ?? "whsec_YOUR_WEBHOOK_SECRET",
    ClientId = builder.Configuration["Stripe:ClientId"] ?? "ca_YOUR_CLIENT_ID"
};
builder.Services.AddSingleton(stripeSettings);

// Add Stripe payment service
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

// Configure Discord settings
var discordSettings = new DiscordSettings
{
    BotToken = builder.Configuration["Discord:BotToken"] ?? "YOUR_BOT_TOKEN_HERE",
    ClientId = builder.Configuration["Discord:ClientId"] ?? "1456365248356286677"
};
builder.Configuration.GetSection("Discord").Bind(discordSettings);
builder.Services.AddSingleton(discordSettings);

// Add Route4 Release Management Service
builder.Services.AddScoped<IReleaseManagementService, ReleaseManagementService>();

// Add Route4 Discord service - conditional based on configuration
builder.Services.AddSingleton<IDiscordBotService>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<DiscordSettings>();
    var logger = serviceProvider.GetRequiredService<ILogger<IDiscordBotService>>();
    
    // Use mock service when configured or when no bot token
    if (settings.UseMockService || string.IsNullOrEmpty(settings.BotToken) || settings.BotToken == "YOUR_BOT_TOKEN_HERE")
    {
        var mockLogger = serviceProvider.GetRequiredService<ILogger<MockDiscordBotService>>();
        logger.LogInformation("Using Mock Discord Service (UseMockService: {UseMock})", settings.UseMockService);
        return new MockDiscordBotService(mockLogger);
    }
    else
    {
        var realLogger = serviceProvider.GetRequiredService<ILogger<RealDiscordBotService>>();
        logger.LogInformation("Using Real Discord Service with token ending in ...{TokenSuffix}", 
            settings.BotToken[^6..]);
        return new RealDiscordBotService(settings, realLogger);
    }
});

// Add CORS for Angular app
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Initialize database
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<Route4DbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        // Ensure database exists and create if needed (preserves existing data)
        context.Database.EnsureCreated();

        // Upgrade CastingCallResponses to use SQL Server temporal history for auditing refreshes
        EnsureCastingCallResponsesTemporal(context, logger);

        logger.LogInformation("Database initialized successfully");
    }
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error seeding database");
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();

// Add custom middleware
app.UseTenantResolution();

app.MapControllers();

app.Run();

static void EnsureCastingCallResponsesTemporal(Route4DbContext context, ILogger logger)
{
    const string enableTemporalSql = @"
IF OBJECT_ID('dbo.CastingCallResponses', 'U') IS NOT NULL
BEGIN
    IF COL_LENGTH('dbo.CastingCallResponses', 'ValidFrom') IS NULL
    BEGIN
        ALTER TABLE dbo.CastingCallResponses
        ADD 
            ValidFrom datetime2(7) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL DEFAULT SYSUTCDATETIME(),
            ValidTo datetime2(7) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL DEFAULT CONVERT(datetime2(7), '9999-12-31 23:59:59.9999999'),
            PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

        ALTER TABLE dbo.CastingCallResponses
        SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CastingCallResponsesHistory, DATA_CONSISTENCY_CHECK = ON));
    END
    ELSE
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'CastingCallResponses' AND temporal_type = 2)
        BEGIN
            ALTER TABLE dbo.CastingCallResponses
            SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CastingCallResponsesHistory, DATA_CONSISTENCY_CHECK = ON));
        END
    END
END";

    try
    {
        context.Database.ExecuteSqlRaw(enableTemporalSql);
        logger.LogInformation("Temporal history ensured for CastingCallResponses");
    }
    catch (Exception temporalEx)
    {
        logger.LogError(temporalEx, "Failed to enable temporal history for CastingCallResponses");
        throw;
    }
}
