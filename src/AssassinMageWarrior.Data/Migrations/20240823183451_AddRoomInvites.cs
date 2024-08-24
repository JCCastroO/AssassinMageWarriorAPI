using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssassinMageWarrior.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomInvites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "Invites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Invites");
        }
    }
}
