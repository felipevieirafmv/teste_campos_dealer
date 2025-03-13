using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace teste_campos_dealer.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClienteData",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteData", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoData",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DscProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VlrUnitario = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoData", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "VendaData",
                columns: table => new
                {
                    IdVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    QtdVenda = table.Column<int>(type: "int", nullable: false),
                    VlrUnitarioVenda = table.Column<float>(type: "real", nullable: false),
                    DthVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VlrTotalVenda = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaData", x => x.IdVenda);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteData");

            migrationBuilder.DropTable(
                name: "ProdutoData");

            migrationBuilder.DropTable(
                name: "VendaData");
        }
    }
}
