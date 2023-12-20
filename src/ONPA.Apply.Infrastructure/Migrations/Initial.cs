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
                name: "application_forms",
                schema: "apply",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    application_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    required_fee_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    paid_fee_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    fee_currency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_forms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recommendations",
                schema: "apply",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recommending_member_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recommending_member_number = table.Column<string>(type: "text", nullable: false),
                    request_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendations", x => x.id);
                });
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
                name: "application_forms",
                schema: "apply");

            migrationBuilder.DropTable(
                name: "recommendations",
                schema: "apply");
        }
    }
}
