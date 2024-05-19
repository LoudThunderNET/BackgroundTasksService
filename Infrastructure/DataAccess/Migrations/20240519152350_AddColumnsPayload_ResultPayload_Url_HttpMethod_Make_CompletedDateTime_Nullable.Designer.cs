﻿// <auto-generated />
using System;
using BackgroundTasksService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackgroundTasksService.DataAccess.Migrations
{
    [DbContext(typeof(TaskSagaStateMachineDbContext))]
    [Migration("20240519152350_AddColumnsPayload_ResultPayload_Url_HttpMethod_Make_CompletedDateTime_Nullable")]
    partial class AddColumnsPayload_ResultPayload_Url_HttpMethod_Make_CompletedDateTime_Nullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackgroundTasksService.AppServices.StateMachines.TaskSaga", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletedDateTime")
                        .HasColumnType("DATETIME2(3)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("DATETIME2(3)");

                    b.Property<int>("CurrentState")
                        .HasColumnType("int");

                    b.Property<string>("HttpMethod")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Payload")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ResultPayload")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("TaskType")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("CorrelationId");

                    b.ToTable("TaskSaga");
                });
#pragma warning restore 612, 618
        }
    }
}