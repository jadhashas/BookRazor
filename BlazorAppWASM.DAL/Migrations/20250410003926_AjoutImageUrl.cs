﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppWASM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AjoutImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Livres",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Livres");
        }
    }
}
