using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProjectTemplate.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReactorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Make ReactorReadingID autoincrement
            migrationBuilder.AlterColumn<int>(
                name: "ReactorReadingID",
                table: "sub_reactor_data",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            // Add ReactorOutput
            migrationBuilder.AddColumn<double>(
                name: "ReactorOutput",
                table: "sub_reactor_data",
                type: "REAL",
                nullable: true);

            // Add Battery
            migrationBuilder.AddColumn<double>(
                name: "Battery",
                table: "sub_reactor_data",
                type: "REAL",
                nullable: true);

            // Add TimeData (non-nullable)
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
            // Remove the added columns
            migrationBuilder.DropColumn(
                name: "ReactorOutput",
                table: "sub_reactor_data");

            migrationBuilder.DropColumn(
                name: "Battery",
                table: "sub_reactor_data");

            migrationBuilder.DropColumn(
                name: "TimeData",
                table: "sub_reactor_data");

            // Revert ReactorReadingID autoincrement change
            migrationBuilder.AlterColumn<int>(
                name: "ReactorReadingID",
                table: "sub_reactor_data",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}