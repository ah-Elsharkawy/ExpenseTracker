using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class fixingRecurrenceNavBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recurrences_AbpUsers_UserId1",
                table: "Recurrences");

            migrationBuilder.DropIndex(
                name: "IX_Recurrences_UserId1",
                table: "Recurrences");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recurrences");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Recurrences",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recurrences_UserId",
                table: "Recurrences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recurrences_AbpUsers_UserId",
                table: "Recurrences",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recurrences_AbpUsers_UserId",
                table: "Recurrences");

            migrationBuilder.DropIndex(
                name: "IX_Recurrences_UserId",
                table: "Recurrences");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Recurrences",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "Recurrences",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recurrences_UserId1",
                table: "Recurrences",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recurrences_AbpUsers_UserId1",
                table: "Recurrences",
                column: "UserId1",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
