using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imageverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertiesNeededForPackageChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PackageValidFrom",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PreviousPackageId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousPackageValidTo",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageValidFrom",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreviousPackageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreviousPackageValidTo",
                table: "Users");
        }
    }
}
