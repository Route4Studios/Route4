using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Route4MoviePlug.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCastingCallInvitationAndAddShortUrlToCastingCall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ThemePrimaryColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ThemeAccentColor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ThemeFontFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThemeFontSize = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentDestructionMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestroyedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestroyedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDestructionMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShortUrls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastingCalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectStatus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ToneAndIntent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RolesDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Constraints = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HowToRespond = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BackgroundImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShortUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastingCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastingCalls_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscordConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuildId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BotToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LanguagePack = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApplicationImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscordConfigurations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceInterval = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TagLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipTiers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseCycleTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseCycleTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseCycleTemplates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SplashPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplashPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplashPages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StripeAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeAccountId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApplicationFeePercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeAccounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CastingCallResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CastingCallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RoleInterest = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastingCallResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CastingCallResponses_CastingCalls_CastingCallId",
                        column: x => x.CastingCallId,
                        principalTable: "CastingCalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscordChannels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscordConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisibilityLevel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessRoom = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscordChannels_DiscordConfigurations_DiscordConfigurationId",
                        column: x => x.DiscordConfigurationId,
                        principalTable: "DiscordConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscordRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscordConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscordRoles_DiscordConfigurations_DiscordConfigurationId",
                        column: x => x.DiscordConfigurationId,
                        principalTable: "DiscordConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipTierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StripeSubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_MembershipTiers_MembershipTierId",
                        column: x => x.MembershipTierId,
                        principalTable: "MembershipTiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TierFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsHighlighted = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TierFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TierFeatures_MembershipTiers_TierId",
                        column: x => x.TierId,
                        principalTable: "MembershipTiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseCycleTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CurrentStage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduledStartAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseInstances_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseInstances_ReleaseCycleTemplates_ReleaseCycleTemplateId",
                        column: x => x.ReleaseCycleTemplateId,
                        principalTable: "ReleaseCycleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseStageTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseCycleTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VisibilityLevel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsDiscordIntegrated = table.Column<bool>(type: "bit", nullable: false),
                    DiscordChannelTemplate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequiresWitnessRole = table.Column<bool>(type: "bit", nullable: false),
                    DurationHours = table.Column<int>(type: "int", nullable: true),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStageTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseStageTemplates_ReleaseCycleTemplates_ReleaseCycleTemplateId",
                        column: x => x.ReleaseCycleTemplateId,
                        principalTable: "ReleaseCycleTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RitualMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseCycleTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RitualName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StageType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionOrder = table.Column<int>(type: "int", nullable: false),
                    TargetChannelPurpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TargetChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisibilityLevel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RequiredRoles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    DefaultDurationHours = table.Column<int>(type: "int", nullable: true),
                    OpenTrigger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseTrigger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomaticallyUnlockChannel = table.Column<bool>(type: "bit", nullable: false),
                    AutomaticallyLockChannel = table.Column<bool>(type: "bit", nullable: false),
                    SlowModeSeconds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisableFileUploads = table.Column<bool>(type: "bit", nullable: false),
                    DisableExternalEmojis = table.Column<bool>(type: "bit", nullable: false),
                    AnnouncementMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ClosingMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CanBeSkipped = table.Column<bool>(type: "bit", nullable: false),
                    NotesForAdmin = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RitualMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RitualMappings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RitualMappings_ReleaseCycleTemplates_ReleaseCycleTemplateId",
                        column: x => x.ReleaseCycleTemplateId,
                        principalTable: "ReleaseCycleTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SplashPageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Benefits_SplashPages_SplashPageId",
                        column: x => x.SplashPageId,
                        principalTable: "SplashPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeInvoiceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PlatformFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ClientAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseArtifacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseInstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kind = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VisibilityLevel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StorageProvider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProviderAssetId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PlaybackEmbedUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DownloadUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseArtifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseArtifacts_ReleaseInstances_ReleaseInstanceId",
                        column: x => x.ReleaseInstanceId,
                        principalTable: "ReleaseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseStageExecutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseInstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStageExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseStageExecutions_ReleaseInstances_ReleaseInstanceId",
                        column: x => x.ReleaseInstanceId,
                        principalTable: "ReleaseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseStateTransitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseInstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromStage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToStage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransitionReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscordChannelsOpened = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DiscordChannelsLocked = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AnnouncementSent = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TriggeredBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStateTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseStateTransitions_ReleaseInstances_ReleaseInstanceId",
                        column: x => x.ReleaseInstanceId,
                        principalTable: "ReleaseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WitnessEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseInstanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscordUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiscordChannelId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WitnessEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WitnessEvents_ReleaseInstances_ReleaseInstanceId",
                        column: x => x.ReleaseInstanceId,
                        principalTable: "ReleaseInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug", "ThemeAccentColor", "ThemeFontFamily", "ThemeFontSize", "ThemePrimaryColor" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Exclusive behind-the-scenes documentary film project", true, "Making of MARY", "making-of-mary", null, null, null, null });

            migrationBuilder.InsertData(
                table: "CastingCalls",
                columns: new[] { "Id", "BackgroundImageUrl", "ClientId", "Constraints", "CreatedAt", "HowToRespond", "IsActive", "ProjectStatus", "RolesDescription", "ShortUrl", "Title", "ToneAndIntent", "UpdatedAt" },
                values: new object[] { new Guid("77777777-7777-7777-7777-777777777777"), "assets/clients/making-of-mary/cover.jpg", new Guid("11111111-1111-1111-1111-111111111111"), "This is not a traditional audition. This is not a networking event. This is an invitation to witness and participate in world formation without exposition.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Responses are accepted through private intake form below. Responses are reviewed deliberately. No social media engagement. No public discussion.", true, "Script complete. Pre-production underway.", "Seeking actors and crew for participation in a process-first production. No outcomes revealed. No character arcs disclosed. Participation is presence, not performance.", null, "Signal I — Casting Call", "Episodic indie project. Restrained. Intentional. No context, only craft.", null });

            migrationBuilder.InsertData(
                table: "MembershipTiers",
                columns: new[] { "Id", "ClientId", "Description", "DisplayOrder", "IsActive", "IsFeatured", "Name", "Price", "PriceInterval", "TagLine" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111"), "Pay per signal witnessed. Discover anonymous pre-artifacts and fragments as they emerge without ongoing subscriptions.", 1, true, false, "Signal Discovery", 7.50m, "per-signal", "Cross the threshold to witness" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("11111111-1111-1111-1111-111111111111"), "Pay per writing table, shot council, or process session attended. Witness the creative process without story outcomes.", 2, true, true, "Ritual Participation", 20.00m, "per-session", "Witness process, not outcomes" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("11111111-1111-1111-1111-111111111111"), "Pay per private viewing event attended. Exclusive offline-safe access in protected locations with verified witness status.", 3, true, false, "Private Viewing", 35.00m, "per-viewing", "Sacred witness moments" }
                });

            migrationBuilder.InsertData(
                table: "ReleaseCycleTemplates",
                columns: new[] { "Id", "ClientId", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Full ritual sequence for Making of Mary episodes", true, "Mary Episode Release Cycle", null });

            migrationBuilder.InsertData(
                table: "SplashPages",
                columns: new[] { "Id", "ClientId", "CreatedAt", "Description", "HeroImageUrl", "IsPublished", "LogoUrl", "Subtitle", "Title", "UpdatedAt" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Get unprecedented access to the entire film making process of MAKING OF MARY. Join our exclusive community and witness every step of the creative journey.", null, true, null, "Go Behind the Scenes", "Exclusive VIP Membership", null });

            migrationBuilder.InsertData(
                table: "Benefits",
                columns: new[] { "Id", "Description", "DisplayOrder", "Icon", "SplashPageId", "Title" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), "Exclusive footage from set, rehearsals, and production meetings", 1, "🎬", new Guid("22222222-2222-2222-2222-222222222222"), "Behind-the-Scenes Content" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), "Access to screenplay drafts, revisions, and director's notes", 2, "📝", new Guid("22222222-2222-2222-2222-222222222222"), "Script Development" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), "Weekly updates from cast and crew throughout filming", 3, "🎥", new Guid("22222222-2222-2222-2222-222222222222"), "Production Diaries" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), "Direct Q&A sessions with filmmakers and cast members", 4, "💬", new Guid("22222222-2222-2222-2222-222222222222"), "Interactive Community" }
                });

            migrationBuilder.InsertData(
                table: "RitualMappings",
                columns: new[] { "Id", "AnnouncementMessage", "AutomaticallyLockChannel", "AutomaticallyUnlockChannel", "CanBeSkipped", "ClientId", "CloseTrigger", "ClosingMessage", "CreatedAt", "DefaultDurationHours", "Description", "DisableExternalEmojis", "DisableFileUploads", "ExecutionOrder", "IsActive", "IsAnonymous", "IsReadOnly", "NotesForAdmin", "OpenTrigger", "ReleaseCycleTemplateId", "RequiredRoles", "RitualName", "SlowModeSeconds", "StageType", "TargetChannelId", "TargetChannelPurpose", "UpdatedAt", "VisibilityLevel" },
                values: new object[,]
                {
                    { new Guid("66666666-1111-1111-1111-111111111111"), "⚒️ **PROCESS OPEN** — The room is now open. Decisions, not outcomes. Craft, not results.", true, true, true, new Guid("11111111-1111-1111-1111-111111111111"), "DurationExpired", "✅ Process session complete. The room is now closed.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Witness-only process rooms (Writing Table, Shot Council, etc.)", false, true, 2, true, false, false, null, "Manual", new Guid("99999999-9999-9999-9999-999999999999"), "[\"CoreTeam\",\"Witness\"]", "Process", "10", "Process", null, "process", null, "L1" },
                    { new Guid("66666666-2222-2222-2222-222222222222"), null, false, false, false, new Guid("11111111-1111-1111-1111-111111111111"), "DurationExpired", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24, "Intentional silence before the drop", false, false, 3, true, false, true, null, "PreviousStageCompleted", new Guid("99999999-9999-9999-9999-999999999999"), null, "Hold", null, "Hold", null, "signal", null, "L2" },
                    { new Guid("66666666-3333-3333-3333-333333333333"), "📺 **THE DROP** — The episode is here. Read-only for 24 hours. Experience it first.", true, true, false, new Guid("11111111-1111-1111-1111-111111111111"), "DurationExpired", "The 24-hour window has closed. Reflection begins.", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24, "Primary episode release", false, false, 4, true, false, true, null, "ScheduledTime", new Guid("99999999-9999-9999-9999-999999999999"), null, "Drop", null, "Drop", null, "releases", null, "L3" },
                    { new Guid("66666666-4444-4444-4444-444444444444"), "💬 **ECHO** — Reflection is open. What did you notice? No spoilers. No theories. Only presence.", false, true, false, new Guid("11111111-1111-1111-1111-111111111111"), "Manual", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 72, "Post-drop reflection (no spoilers, no theories)", false, true, 5, true, false, false, null, "PreviousStageCompleted", new Guid("99999999-9999-9999-9999-999999999999"), "[\"Witness\",\"Member\"]", "Echo", "30", "Echo", null, "reflection", null, "L3" },
                    { new Guid("66666666-5555-5555-5555-555555555555"), "🎬 **FRAGMENTS** — Behind-the-scenes residue. No context. Just artifacts.", false, false, true, new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "BTS residue and artifacts", false, false, 6, true, false, true, null, "Manual", new Guid("99999999-9999-9999-9999-999999999999"), "[\"CoreTeam\"]", "Fragments", null, "Fragments", null, "fragments", null, "L2" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "🎙️ **INTERVAL** — Meta reflection podcast is live.", false, true, true, new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Podcast and meta reflection", false, false, 7, true, false, true, null, "Manual", new Guid("99999999-9999-9999-9999-999999999999"), null, "Interval", null, "Interval", null, "interval", null, "L3" },
                    { new Guid("66666666-7777-7777-7777-777777777777"), "🎟️ **PRIVATE VIEWING** — Invitations for Witness-only screening have been sent.", false, false, true, new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Witness-only private screening", false, false, 8, true, false, true, null, "Manual", new Guid("99999999-9999-9999-9999-999999999999"), "[\"Witness\"]", "PrivateViewing", null, "PrivateViewing", null, "invitations", null, "L1" },
                    { new Guid("66666666-8888-8888-8888-888888888888"), null, false, false, false, new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Permanent record and canon", false, false, 9, true, false, true, null, "PreviousStageCompleted", new Guid("99999999-9999-9999-9999-999999999999"), null, "Archive", null, "Archive", null, "releases", null, "L3" }
                });

            migrationBuilder.InsertData(
                table: "TierFeatures",
                columns: new[] { "Id", "DisplayOrder", "IsHighlighted", "Text", "TierId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000001"), 1, true, "Anonymous signal drops as they emerge", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000002"), 2, false, "BTS fragments without story context", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000003"), 3, false, "Tools, hands, rooms — no explanations", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000004"), 4, false, "Pay only when you choose to witness", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000005"), 5, false, "No subscriptions, no ongoing fees", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000006"), 1, true, "Writing table sessions (decisions, not outcomes)", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000007"), 2, true, "Shot council participation (process preview)", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000008"), 3, false, "Color/edit sessions without story reveals", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-000000000009"), 4, false, "Reflection channels after ritual participation", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000a"), 5, false, "Pay per session attended, not time-based", new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000b"), 1, true, "Private location screenings (QR-based access)", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000c"), 2, false, "Offline-safe viewing with verified witness status", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000d"), 3, false, "No recording, no sharing — sacred witness moments", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000e"), 4, false, "Invitation-only events for proven participants", new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-00000000000f"), 5, false, "Pay only when invited and you choose to attend", new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benefits_SplashPageId",
                table: "Benefits",
                column: "SplashPageId");

            migrationBuilder.CreateIndex(
                name: "IX_CastingCallResponses_CastingCallId",
                table: "CastingCallResponses",
                column: "CastingCallId");

            migrationBuilder.CreateIndex(
                name: "IX_CastingCalls_ClientId",
                table: "CastingCalls",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Slug",
                table: "Clients",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscordChannels_DiscordConfigurationId_ChannelId",
                table: "DiscordChannels",
                columns: new[] { "DiscordConfigurationId", "ChannelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscordConfigurations_ClientId",
                table: "DiscordConfigurations",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscordRoles_DiscordConfigurationId_RoleId",
                table: "DiscordRoles",
                columns: new[] { "DiscordConfigurationId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentId",
                table: "Invoices",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StripeInvoiceId",
                table: "Invoices",
                column: "StripeInvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipTiers_ClientId",
                table: "MembershipTiers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MembershipTierId",
                table: "Payments",
                column: "MembershipTierId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StripePaymentIntentId",
                table: "Payments",
                column: "StripePaymentIntentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseArtifacts_ReleaseInstanceId",
                table: "ReleaseArtifacts",
                column: "ReleaseInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseCycleTemplates_ClientId",
                table: "ReleaseCycleTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInstances_ClientId_Key",
                table: "ReleaseInstances",
                columns: new[] { "ClientId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInstances_ReleaseCycleTemplateId",
                table: "ReleaseInstances",
                column: "ReleaseCycleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseStageExecutions_ReleaseInstanceId",
                table: "ReleaseStageExecutions",
                column: "ReleaseInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseStageTemplates_ReleaseCycleTemplateId",
                table: "ReleaseStageTemplates",
                column: "ReleaseCycleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseStateTransitions_OccurredAt",
                table: "ReleaseStateTransitions",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseStateTransitions_ReleaseInstanceId",
                table: "ReleaseStateTransitions",
                column: "ReleaseInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_RitualMappings_ClientId_ReleaseCycleTemplateId_RitualName",
                table: "RitualMappings",
                columns: new[] { "ClientId", "ReleaseCycleTemplateId", "RitualName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RitualMappings_ReleaseCycleTemplateId",
                table: "RitualMappings",
                column: "ReleaseCycleTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_ShortCode",
                table: "ShortUrls",
                column: "ShortCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SplashPages_ClientId",
                table: "SplashPages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccounts_ClientId",
                table: "StripeAccounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccounts_StripeAccountId",
                table: "StripeAccounts",
                column: "StripeAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TierFeatures_TierId",
                table: "TierFeatures",
                column: "TierId");

            migrationBuilder.CreateIndex(
                name: "IX_WitnessEvents_ReleaseInstanceId",
                table: "WitnessEvents",
                column: "ReleaseInstanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "CastingCallResponses");

            migrationBuilder.DropTable(
                name: "DiscordChannels");

            migrationBuilder.DropTable(
                name: "DiscordRoles");

            migrationBuilder.DropTable(
                name: "DocumentDestructionMetadata");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ReleaseArtifacts");

            migrationBuilder.DropTable(
                name: "ReleaseStageExecutions");

            migrationBuilder.DropTable(
                name: "ReleaseStageTemplates");

            migrationBuilder.DropTable(
                name: "ReleaseStateTransitions");

            migrationBuilder.DropTable(
                name: "RitualMappings");

            migrationBuilder.DropTable(
                name: "ShortUrls");

            migrationBuilder.DropTable(
                name: "StripeAccounts");

            migrationBuilder.DropTable(
                name: "TierFeatures");

            migrationBuilder.DropTable(
                name: "WitnessEvents");

            migrationBuilder.DropTable(
                name: "SplashPages");

            migrationBuilder.DropTable(
                name: "CastingCalls");

            migrationBuilder.DropTable(
                name: "DiscordConfigurations");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ReleaseInstances");

            migrationBuilder.DropTable(
                name: "MembershipTiers");

            migrationBuilder.DropTable(
                name: "ReleaseCycleTemplates");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
