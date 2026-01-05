using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route4MoviePlug.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOutreachAIPhaseI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OutreachContactId",
                table: "WitnessEvents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralSource",
                table: "WitnessEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtmCampaign",
                table: "WitnessEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtmSource",
                table: "WitnessEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistributionMetadata",
                table: "ReleaseInstances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OutreachCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CastingCallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TierLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetWitnessCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CuratedCommunitiesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScriptsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostingScheduleJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalContacts = table.Column<int>(type: "int", nullable: false),
                    TotalClicks = table.Column<int>(type: "int", nullable: false),
                    TotalConversions = table.Column<int>(type: "int", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPerWitness = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutreachCampaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutreachCommunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmissionFormUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialHandle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenresJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TagsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedReach = table.Column<int>(type: "int", nullable: true),
                    PostingRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresApproval = table.Column<bool>(type: "bit", nullable: false),
                    ComplianceNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasCaptcha = table.Column<bool>(type: "bit", nullable: false),
                    TotalOutreachAttempts = table.Column<int>(type: "int", nullable: false),
                    LastContactedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuccessfulConversions = table.Column<int>(type: "int", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FormFieldMapJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutreachCommunities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutreachContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CastingCallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortUrlVariant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClickedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConvertedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalClicks = table.Column<int>(type: "int", nullable: false),
                    DidConvert = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutreachContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutreachContacts_OutreachCommunities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "OutreachCommunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutreachContacts_CommunityId",
                table: "OutreachContacts",
                column: "CommunityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutreachCampaigns");

            migrationBuilder.DropTable(
                name: "OutreachContacts");

            migrationBuilder.DropTable(
                name: "OutreachCommunities");

            migrationBuilder.DropColumn(
                name: "OutreachContactId",
                table: "WitnessEvents");

            migrationBuilder.DropColumn(
                name: "ReferralSource",
                table: "WitnessEvents");

            migrationBuilder.DropColumn(
                name: "UtmCampaign",
                table: "WitnessEvents");

            migrationBuilder.DropColumn(
                name: "UtmSource",
                table: "WitnessEvents");

            migrationBuilder.DropColumn(
                name: "DistributionMetadata",
                table: "ReleaseInstances");
        }
    }
}
