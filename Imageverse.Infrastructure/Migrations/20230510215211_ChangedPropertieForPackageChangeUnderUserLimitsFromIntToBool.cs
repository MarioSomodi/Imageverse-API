using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imageverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertieForPackageChangeUnderUserLimitsFromIntToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "RequestedChangeOfPackage",
                table: "UserLimits",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RequestedChangeOfPackage",
                table: "UserLimits",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
