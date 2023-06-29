using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imageverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeToUserAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "UserActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "UserActions");
        }
    }
}
