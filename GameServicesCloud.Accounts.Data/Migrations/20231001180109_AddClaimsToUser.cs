using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameServicesCloud.Accounts.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountClaim",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountClaimUser",
                columns: table => new
                {
                    ClaimsId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClaimUser", x => new { x.ClaimsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AccountClaimUser_AccountClaim_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "AccountClaim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountClaimUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClaimUser_UserId",
                table: "AccountClaimUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClaimUser");

            migrationBuilder.DropTable(
                name: "AccountClaim");
        }
    }
}
