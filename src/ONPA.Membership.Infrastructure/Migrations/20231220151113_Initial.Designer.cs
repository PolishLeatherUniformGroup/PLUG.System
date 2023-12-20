﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ONPA.Membership.Infrastructure.Database;

#nullable disable

namespace ONPA.Membership.Infrastructure.Migrations
{
    [DbContext(typeof(MembershipContext))]
    [Migration("20231220151113_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "vector");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ONPA.Common.Infrastructure.StateEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid");

                    b.Property<string>("AggregateTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("EventId");

                    b.HasIndex("TenantId", "AggregateId");

                    b.ToTable("AggregateStream", "membership");
                });

            modelBuilder.Entity("ONPA.IntegrationEventsLog.IntegrationEventLogEntry", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("TimesSent")
                        .HasColumnType("integer");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.HasKey("EventId");

                    b.ToTable("IntegrationEventLog", "membership");
                });

            modelBuilder.Entity("ONPA.Membership.Infrastructure.ReadModel.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MemberNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MembershipType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("MembershipValidUntil")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TerminationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TerminationReason")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Member", "membership");
                });

            modelBuilder.Entity("ONPA.Membership.Infrastructure.ReadModel.MemberExpel", b =>
                {
                    b.Property<DateTime>("ExpelDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("AppealDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("AppealDecisionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AppealJustification")
                        .HasColumnType("text");

                    b.Property<string>("Justification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.HasKey("ExpelDate");

                    b.ToTable("MemberExpel", "membership");
                });

            modelBuilder.Entity("ONPA.Membership.Infrastructure.ReadModel.MemberFee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DueAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FeeEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("PaidAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("MemberFee", "membership");
                });

            modelBuilder.Entity("ONPA.Membership.Infrastructure.ReadModel.MemberSuspension", b =>
                {
                    b.Property<DateTime>("SuspensionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("AppealDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("AppealDecisionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AppealJustification")
                        .HasColumnType("text");

                    b.Property<string>("Justification")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SuspendedUntil")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("SuspensionDate");

                    b.ToTable("MemberSuspension", "membership");
                });
#pragma warning restore 612, 618
        }
    }
}
