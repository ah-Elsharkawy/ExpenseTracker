using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class Budget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_AbpUsers_UserId1",
                table: "UserCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCategories",
                table: "UserCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_UserId1",
                table: "UserCategories");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCategories");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserCategories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "AmountSpent",
                table: "UserCategories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCategories",
                table: "UserCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_AbpUsers_UserId",
                table: "UserCategories",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_AbpUsers_UserId",
                table: "UserCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCategories",
                table: "UserCategories");

            migrationBuilder.DropIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCategories");

            migrationBuilder.DropColumn(
                name: "AmountSpent",
                table: "UserCategories");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCategories",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "UserCategories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCategories",
                table: "UserCategories",
                columns: new[] { "UserId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId1",
                table: "UserCategories",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_AbpUsers_UserId1",
                table: "UserCategories",
                column: "UserId1",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
