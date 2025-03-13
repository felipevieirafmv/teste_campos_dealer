using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teste_campos_dealer.Migrations
{
    /// <inheritdoc />
    public partial class RenameProdutoAndVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VlrUnitarioVenda",
                table: "VendaData",
                newName: "vlrUnitarioVenda");

            migrationBuilder.RenameColumn(
                name: "VlrTotalVenda",
                table: "VendaData",
                newName: "vlrTotalVenda");

            migrationBuilder.RenameColumn(
                name: "QtdVenda",
                table: "VendaData",
                newName: "qtdVenda");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "VendaData",
                newName: "idProduto");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "VendaData",
                newName: "idCliente");

            migrationBuilder.RenameColumn(
                name: "DthVenda",
                table: "VendaData",
                newName: "dthVenda");

            migrationBuilder.RenameColumn(
                name: "VlrUnitario",
                table: "ProdutoData",
                newName: "vlrUnitario");

            migrationBuilder.RenameColumn(
                name: "DscProduto",
                table: "ProdutoData",
                newName: "dscProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vlrUnitarioVenda",
                table: "VendaData",
                newName: "VlrUnitarioVenda");

            migrationBuilder.RenameColumn(
                name: "vlrTotalVenda",
                table: "VendaData",
                newName: "VlrTotalVenda");

            migrationBuilder.RenameColumn(
                name: "qtdVenda",
                table: "VendaData",
                newName: "QtdVenda");

            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "VendaData",
                newName: "IdProduto");

            migrationBuilder.RenameColumn(
                name: "idCliente",
                table: "VendaData",
                newName: "IdCliente");

            migrationBuilder.RenameColumn(
                name: "dthVenda",
                table: "VendaData",
                newName: "DthVenda");

            migrationBuilder.RenameColumn(
                name: "vlrUnitario",
                table: "ProdutoData",
                newName: "VlrUnitario");

            migrationBuilder.RenameColumn(
                name: "dscProduto",
                table: "ProdutoData",
                newName: "DscProduto");
        }
    }
}
