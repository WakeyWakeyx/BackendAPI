using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WakeyWakeyBackendAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSleepDurationAndHeartRateToSleepSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageHeartRate",
                table: "SleepSessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DeepSleepDuration",
                table: "SleepSessions",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "LightSleepDuration",
                table: "SleepSessions",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RemSleepDuration",
                table: "SleepSessions",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageHeartRate",
                table: "SleepSessions");

            migrationBuilder.DropColumn(
                name: "DeepSleepDuration",
                table: "SleepSessions");

            migrationBuilder.DropColumn(
                name: "LightSleepDuration",
                table: "SleepSessions");

            migrationBuilder.DropColumn(
                name: "RemSleepDuration",
                table: "SleepSessions");
        }
    }
}
