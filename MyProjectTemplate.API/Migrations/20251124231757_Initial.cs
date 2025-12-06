using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProjectTemplate.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sub_data",
                columns: table => new
                {
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.SubID);
                });

            migrationBuilder.CreateTable(
                name: "sub_alarms_data",
                columns: table => new
                {
                    AlarmID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlarmName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SeverityLevel = table.Column<int>(type: "INTEGER", nullable: true),
                    RaisedAt = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    ClearedAt = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.AlarmID);
                    table.ForeignKey(
                        name: "sub_alarms_data_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "sub_control_data",
                columns: table => new
                {
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeData = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    PropellerState = table.Column<double>(type: "REAL", nullable: false),
                    RudderState = table.Column<double>(type: "REAL", nullable: false),
                    SternPlateState = table.Column<double>(type: "REAL", nullable: false),
                    BallastState = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.SubID, x.TimeData });
                    table.ForeignKey(
                        name: "sub_control_data_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "sub_life_support_data",
                columns: table => new
                {
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeData = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    O2Level = table.Column<double>(type: "REAL", nullable: true),
                    CO2Level = table.Column<double>(type: "REAL", nullable: true),
                    AirTanklevel = table.Column<double>(type: "REAL", nullable: true),
                    InternalPressure = table.Column<double>(type: "REAL", nullable: true),
                    ExternalPressure = table.Column<double>(type: "REAL", nullable: true),
                    Temperature = table.Column<double>(type: "REAL", nullable: true),
                    Humidity = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.SubID, x.TimeData });
                    table.ForeignKey(
                        name: "sub_life_support_data_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "sub_logs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Level = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PerformedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    TimeData = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.LogID);
                    table.ForeignKey(
                        name: "sub_logs_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "sub_position_data",
                columns: table => new
                {
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeData = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.SubID, x.TimeData });
                    table.ForeignKey(
                        name: "sub_position_data_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateTable(
                name: "sub_reactor_data",
                columns: table => new
                {
                    ReactorID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CoolantLevel = table.Column<double>(type: "REAL", nullable: true),
                    Temperature = table.Column<double>(type: "REAL", nullable: true),
                    Radiation = table.Column<double>(type: "REAL", nullable: true),
                    FuelRodStatus = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ReactorID);
                    table.ForeignKey(
                        name: "sub_reactor_data_ibfk_1",
                        column: x => x.SubID,
                        principalTable: "sub_data",
                        principalColumn: "SubID");
                });

            migrationBuilder.CreateIndex(
                name: "SubID",
                table: "sub_alarms_data",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "SubID1",
                table: "sub_logs",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "SubID2",
                table: "sub_reactor_data",
                column: "SubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sub_alarms_data");

            migrationBuilder.DropTable(
                name: "sub_control_data");

            migrationBuilder.DropTable(
                name: "sub_life_support_data");

            migrationBuilder.DropTable(
                name: "sub_logs");

            migrationBuilder.DropTable(
                name: "sub_position_data");

            migrationBuilder.DropTable(
                name: "sub_reactor_data");

            migrationBuilder.DropTable(
                name: "sub_data");
        }
    }
}
