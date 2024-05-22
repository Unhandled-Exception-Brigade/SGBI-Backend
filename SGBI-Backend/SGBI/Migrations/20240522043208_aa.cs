using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGBI.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CantidadTrimestre",
                table: "ExoneracionesInformacion",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<double>(
                name: "Excedente",
                table: "ExoneracionesInformacion",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MontoExonerar",
                table: "ExoneracionesInformacion",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excedente",
                table: "ExoneracionesInformacion");

            migrationBuilder.DropColumn(
                name: "MontoExonerar",
                table: "ExoneracionesInformacion");

            migrationBuilder.AlterColumn<int>(
                name: "CantidadTrimestre",
                table: "ExoneracionesInformacion",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
