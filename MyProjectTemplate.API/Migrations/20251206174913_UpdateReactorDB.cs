using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProjectTemplate.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReactorDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Battery",
                table: "sub_reactor_data",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ReactorOutput",
                table: "sub_reactor_data",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeData",
                table: "sub_reactor_data",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battery",
                table: "sub_reactor_data");

            migrationBuilder.DropColumn(
                name: "ReactorOutput",
                table: "sub_reactor_data");

            migrationBuilder.DropColumn(
                name: "TimeData",
                table: "sub_reactor_data");
        }
    }
}
