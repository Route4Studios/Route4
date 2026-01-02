using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route4MoviePlug.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Let EF Core create the schema from the model
            // This migration is intentionally empty - we'll use EnsureCreated on first run
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No down migration needed
        }
    }
}
