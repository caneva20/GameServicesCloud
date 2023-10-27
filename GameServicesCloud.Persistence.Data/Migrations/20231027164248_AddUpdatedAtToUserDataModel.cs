using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameServicesCloud.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToUserDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserDatas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserDatas");
        }
    }
}
