using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imageverse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedPackageValidToPropertyFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousPackageValidTo",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousPackageValidTo",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
