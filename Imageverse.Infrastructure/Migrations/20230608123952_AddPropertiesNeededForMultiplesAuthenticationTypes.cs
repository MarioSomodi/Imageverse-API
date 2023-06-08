using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imageverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesNeededForMultiplesAuthenticationTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticationProviderId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthenticationType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthenticationProviderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthenticationType",
                table: "Users");
        }
    }
}
