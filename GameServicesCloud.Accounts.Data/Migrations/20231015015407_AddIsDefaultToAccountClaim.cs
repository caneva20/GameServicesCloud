using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameServicesCloud.Accounts.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultToAccountClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AccountClaim",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AccountClaim");
        }
    }
}
