using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONPA.Organizations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "org");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "AggregateStream",
                schema: "org",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
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
                schema: "org",
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
                name: "OrganizationFees",
                schema: "org",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(8,2)", precision: 8, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationFees", x => new { x.OrganizationId, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSettings",
                schema: "org",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredRecommendations = table.Column<int>(type: "integer", nullable: false),
                    DaysForAppeal = table.Column<int>(type: "integer", nullable: false),
                    FeePaymentMonth = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSettings", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "org",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CardPrefix = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    TaxId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    AccountNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Regon = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ContactEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregateStream",
                schema: "org");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "org");

            migrationBuilder.DropTable(
                name: "OrganizationFees",
                schema: "org");

            migrationBuilder.DropTable(
                name: "OrganizationSettings",
                schema: "org");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "org");
        }
    }
}
