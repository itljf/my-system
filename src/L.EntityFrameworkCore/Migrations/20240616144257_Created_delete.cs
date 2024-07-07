using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L.Migrations
{
    /// <inheritdoc />
    public partial class Created_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TbInfoTagItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TbInfoTagItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TbInfoTagItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TbInfoTag",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TbInfoTag",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TbInfoTag",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TbInformation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TbInformation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TbInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TbInfoGroupItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TbInfoGroupItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TbInfoGroupItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TbInfoGroup",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TbInfoGroup",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TbInfoGroup",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TbInfoTagItem");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TbInfoTagItem");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TbInfoTagItem");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TbInfoTag");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TbInfoTag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TbInfoTag");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TbInformation");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TbInformation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TbInformation");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TbInfoGroupItem");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TbInfoGroupItem");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TbInfoGroupItem");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TbInfoGroup");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TbInfoGroup");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TbInfoGroup");
        }
    }
}
