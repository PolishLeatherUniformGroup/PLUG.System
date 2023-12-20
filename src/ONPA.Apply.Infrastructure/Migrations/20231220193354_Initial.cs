using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONPA.Apply.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apply");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "AggregateStream",
                schema: "apply",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateTypeName = table.Column<string>(type: "text", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregateStream", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "apply",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "application_actions",
                schema: "apply",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DecisionJustification = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_actions", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "application_forms",
                schema: "apply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequiredFeeAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaidFeeAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    FeeCurrency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                schema: "apply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecommendingMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecommendingMemberNumber = table.Column<string>(type: "text", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AggregateStream_TenantId_AggregateId",
                schema: "apply",
                table: "AggregateStream",
                columns: new[] { "TenantId", "AggregateId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregateStream",
                schema: "apply");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "apply");

            migrationBuilder.DropTable(
                name: "application_actions",
                schema: "apply");

            migrationBuilder.DropTable(
                name: "application_forms",
                schema: "apply");

            migrationBuilder.DropTable(
                name: "recommendations",
                schema: "apply");
        }
    }
}
