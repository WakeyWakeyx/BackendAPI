using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WakeyWakeyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyAlarmAttributeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepeatingDays",
                table: "Alarms",
                newName: "DaysToRepeat");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "Alarms",
                newName: "Enabled");

            migrationBuilder.RenameColumn(
                name: "AlarmName",
                table: "Alarms",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AlarmId",
                table: "Alarms",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Alarms",
                newName: "AlarmName");

            migrationBuilder.RenameColumn(
                name: "Enabled",
                table: "Alarms",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "DaysToRepeat",
                table: "Alarms",
                newName: "RepeatingDays");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Alarms",
                newName: "AlarmId");
        }
    }
}
