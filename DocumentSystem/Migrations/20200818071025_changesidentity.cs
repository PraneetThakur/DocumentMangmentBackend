using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentSystem.Migrations
{
    public partial class changesidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "MUserMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityIds",
                table: "MUserMaster",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MUserMaster_IdentityId",
                table: "MUserMaster",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MUserMaster_AspNetUsers_IdentityId",
                table: "MUserMaster",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MUserMaster_AspNetUsers_IdentityId",
                table: "MUserMaster");

            migrationBuilder.DropIndex(
                name: "IX_MUserMaster_IdentityId",
                table: "MUserMaster");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "MUserMaster");

            migrationBuilder.DropColumn(
                name: "IdentityIds",
                table: "MUserMaster");
        }
    }
}
