using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherConditions.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "Date", nullable: true),
                    Time = table.Column<TimeSpan>(type: "Time", nullable: true),
                    Temperature = table.Column<float>(type: "real", nullable: true),
                    AirHumidity = table.Column<int>(type: "int", nullable: true),
                    DewPoint = table.Column<float>(type: "real", nullable: true),
                    Pressure = table.Column<int>(type: "int", nullable: true),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpeedWind = table.Column<int>(type: "int", nullable: true),
                    CloudCover = table.Column<int>(type: "int", nullable: true),
                    LowerCloudLimit = table.Column<int>(type: "int", nullable: true),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
