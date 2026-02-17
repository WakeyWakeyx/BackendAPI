using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WakeyWakeyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifySleepSessionIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "SleepSessions",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SleepSessions",
                newName: "SessionId");
        }
    }
}
