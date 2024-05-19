using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackgroundTasksService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsPayload_ResultPayload_Url_HttpMethod_Make_CompletedDateTime_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDateTime",
                table: "TaskSaga",
                type: "DATETIME2(3)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(3)");

            migrationBuilder.AddColumn<string>(
                name: "HttpMethod",
                table: "TaskSaga",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                table: "TaskSaga",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultPayload",
                table: "TaskSaga",
                type: "varchar(max)",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TaskSaga",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HttpMethod",
                table: "TaskSaga");

            migrationBuilder.DropColumn(
                name: "Payload",
                table: "TaskSaga");

            migrationBuilder.DropColumn(
                name: "ResultPayload",
                table: "TaskSaga");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TaskSaga");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDateTime",
                table: "TaskSaga",
                type: "DATETIME2(3)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(3)",
                oldNullable: true);
        }
    }
}
