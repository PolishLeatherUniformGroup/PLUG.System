using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONPA.Gatherings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gatherings");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "AggregateStream",
                schema: "gatherings",
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
                name: "Event",
                schema: "gatherings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Regulations = table.Column<string>(type: "text", nullable: false),
                    ScheduledStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlannedCapacity = table.Column<int>(type: "integer", nullable: true),
                    AvailablePlaces = table.Column<int>(type: "integer", nullable: true),
                    PricePerPerson = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnrollmentDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                schema: "gatherings",
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
                name: "enrollments",
                schema: "gatherings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlacesBooked = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    RequiredPaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    PaidDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancellationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RefundableAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    RefundedAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    RefundDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "event_participants",
                schema: "gatherings",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_participants", x => x.Email);
                    table.ForeignKey(
                        name: "FK_event_participants_enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalSchema: "gatherings",
                        principalTable: "enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AggregateStream_TenantId_AggregateId",
                schema: "gatherings",
                table: "AggregateStream",
                columns: new[] { "TenantId", "AggregateId" });

            migrationBuilder.CreateIndex(
                name: "IX_event_participants_EnrollmentId",
                schema: "gatherings",
                table: "event_participants",
                column: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregateStream",
                schema: "gatherings");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "gatherings");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog",
                schema: "gatherings");

            migrationBuilder.DropTable(
                name: "event_participants",
                schema: "gatherings");

            migrationBuilder.DropTable(
                name: "enrollments",
                schema: "gatherings");
        }
    }
}
