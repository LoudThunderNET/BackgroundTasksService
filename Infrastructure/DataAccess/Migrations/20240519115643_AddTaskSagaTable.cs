using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgroundTasksService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskSagaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskSaga",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    TaskType = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "DATETIME2(3)", nullable: false),
                    CompletedDateTime = table.Column<DateTime>(type: "DATETIME2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSaga", x => x.CorrelationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSaga");
        }
    }
}
