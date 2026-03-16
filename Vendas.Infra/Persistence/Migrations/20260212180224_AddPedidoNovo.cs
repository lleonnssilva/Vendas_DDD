using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendas.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPedidoNovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_Pedidos_PedidoId",
                table: "Pagamento");

            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pagamento",
                table: "Pagamento");

            migrationBuilder.RenameTable(
                name: "Pagamento",
                newName: "PedidoPagamentos");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Pais",
                table: "Pedidos",
                newName: "Pais");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Numero",
                table: "Pedidos",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Logradouro",
                table: "Pedidos",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Estado",
                table: "Pedidos",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Complemento",
                table: "Pedidos",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Cidade",
                table: "Pedidos",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Cep",
                table: "Pedidos",
                newName: "Cep");

            migrationBuilder.RenameColumn(
                name: "EnderecoEntrega_Bairro",
                table: "Pedidos",
                newName: "Bairro");

            migrationBuilder.RenameIndex(
                name: "IX_Pagamento_PedidoId",
                table: "PedidoPagamentos",
                newName: "IX_PedidoPagamentos_PedidoId");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroPedido",
                table: "Pedidos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "StatusPagamento",
                table: "PedidoPagamentos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MetodoPagamento",
                table: "PedidoPagamentos",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoPagamentos",
                table: "PedidoPagamentos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DescontoAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_NumeroPedido",
                table: "Pedidos",
                column: "NumeroPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoId",
                table: "PedidoItens",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoPagamentos_Pedidos_PedidoId",
                table: "PedidoPagamentos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoPagamentos_Pedidos_PedidoId",
                table: "PedidoPagamentos");

            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_NumeroPedido",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoPagamentos",
                table: "PedidoPagamentos");

            migrationBuilder.RenameTable(
                name: "PedidoPagamentos",
                newName: "Pagamento");

            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "Pedidos",
                newName: "EnderecoEntrega_Pais");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Pedidos",
                newName: "EnderecoEntrega_Numero");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Pedidos",
                newName: "EnderecoEntrega_Logradouro");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Pedidos",
                newName: "EnderecoEntrega_Estado");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Pedidos",
                newName: "EnderecoEntrega_Complemento");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Pedidos",
                newName: "EnderecoEntrega_Cidade");

            migrationBuilder.RenameColumn(
                name: "Cep",
                table: "Pedidos",
                newName: "EnderecoEntrega_Cep");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Pedidos",
                newName: "EnderecoEntrega_Bairro");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoPagamentos_PedidoId",
                table: "Pagamento",
                newName: "IX_Pagamento_PedidoId");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroPedido",
                table: "Pedidos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "StatusPagamento",
                table: "Pagamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "MetodoPagamento",
                table: "Pagamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pagamento",
                table: "Pagamento",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescontoAplicado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_Pedidos_PedidoId",
                table: "Pagamento",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
