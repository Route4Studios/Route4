using Microsoft.EntityFrameworkCore;
using Route4MoviePlug.Api.Data;
using Route4MoviePlug.Api.Middleware;
using Route4MoviePlug.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add DbContext with In-Memory database for now
builder.Services.AddDbContext<Route4DbContext>(options =>
    options.UseInMemoryDatabase("Route4Studios"));

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

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Route4DbContext>();
    context.Database.EnsureCreated();
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
