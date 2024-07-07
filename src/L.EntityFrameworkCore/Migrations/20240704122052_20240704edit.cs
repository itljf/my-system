using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L.Migrations
{
    /// <inheritdoc />
    public partial class _20240704edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "TbInfoTag",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "TbInfoTag");
        }
    }
}
