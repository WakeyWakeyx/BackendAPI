using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WakeyWakeyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameSleepSessionTimeIntervals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "SleepSessions",
                newName: "WakeTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "SleepSessions",
                newName: "BedTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WakeTime",
                table: "SleepSessions",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "BedTime",
                table: "SleepSessions",
                newName: "EndTime");
        }
    }
}
