using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teste_campos_dealer.Migrations
{
    /// <inheritdoc />
    public partial class RenameAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NmCliente",
                table: "ClienteData",
                newName: "nmCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nmCliente",
                table: "ClienteData",
                newName: "NmCliente");
        }
    }
}
