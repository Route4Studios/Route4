using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Middleware;
using Route4MoviePlug.Api.Services;
using Route4MoviePlug.Api.Services.Discord;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services (REGISTER FIRST)
// --------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger (Swashbuckle)
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Route4 API",
        Version = "v1",
        Description = "Internal API docs for Route4 (admin + orchestration)."
    });
    
    // Optional: show XML comments (only if file exists)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// EF Core (you didn’t show DbContext registration earlier—ensure it exists)
builder.Services.AddDbContext<Route4DbContext>(options =>
{
    // TODO: set your provider here, example:
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Stripe config
var stripeSettings = new StripeSettings
{
    SecretKey = builder.Configuration["Stripe:SecretKey"] ?? "sk_test_YOUR_TEST_SECRET_KEY",
    PublishableKey = builder.Configuration["Stripe:PublishableKey"] ?? "pk_test_YOUR_TEST_PUBLISHABLE_KEY",
    WebhookSecret = builder.Configuration["Stripe:WebhookSecret"] ?? "whsec_YOUR_WEBHOOK_SECRET",
    ClientId = builder.Configuration["Stripe:ClientId"] ?? "ca_YOUR_CLIENT_ID"
};
builder.Services.AddSingleton(stripeSettings);
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();

// Discord settings
var discordSettings = new DiscordSettings
{
    BotToken = builder.Configuration["Discord:BotToken"] ?? "YOUR_BOT_TOKEN_HERE",
    ClientId = builder.Configuration["Discord:ClientId"] ?? "1456365248356286677"
};
builder.Configuration.GetSection("Discord").Bind(discordSettings);
builder.Services.AddSingleton(discordSettings);

builder.Services.AddScoped<IReleaseManagementService, ReleaseManagementService>();

// Discord bot service (conditional)
builder.Services.AddSingleton<IDiscordBotService>(sp =>
{
    var settings = sp.GetRequiredService<DiscordSettings>();
    var logger = sp.GetRequiredService<ILogger<IDiscordBotService>>();

    if (settings.UseMockService || string.IsNullOrEmpty(settings.BotToken) || settings.BotToken == "YOUR_BOT_TOKEN_HERE")
    {
        var mockLogger = sp.GetRequiredService<ILogger<MockDiscordBotService>>();
        logger.LogInformation("Using Mock Discord Service (UseMockService: {UseMock})", settings.UseMockService);
        return new MockDiscordBotService(mockLogger);
    }

    var realLogger = sp.GetRequiredService<ILogger<RealDiscordBotService>>();
    logger.LogInformation("Using Real Discord Service with token ending in ...{TokenSuffix}", settings.BotToken[^6..]);
    return new RealDiscordBotService(settings, realLogger);
});

// CORS for Angular
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// --------------------
// Build (ONLY ONCE)
// --------------------
var app = builder.Build();

// --------------------
// Startup tasks (DB init, etc.) BEFORE Run
// --------------------
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<Route4DbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    context.Database.EnsureCreated();
    EnsureCastingCallResponsesTemporal(context, logger);

    logger.LogInformation("Database initialized successfully");
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error initializing database");
    throw;
}

// --------------------
// Middleware / Pipeline (ONCE)
// --------------------

// Static files for Swagger UI
app.UseStaticFiles();

// Swagger endpoints
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Route4 API v1");
        options.RoutePrefix = string.Empty; // Serve at root: /
    });
}

// Only redirect to HTTPS in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors();

// Custom middleware for tenant resolution <slug> extraction
app.UseTenantResolution();

app.UseAuthorization();

app.MapControllers();

app.Run();

// --------------------
// Helpers
// --------------------
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

    context.Database.ExecuteSqlRaw(enableTemporalSql);
    logger.LogInformation("Temporal history ensured for CastingCallResponses");
}
