using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AndreAirlaines_API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aeronave",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(10)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeronave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "varchar(150)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: true),
                    Localidade = table.Column<string>(type: "varchar(30)", nullable: true),
                    Uf = table.Column<string>(type: "varchar(2)", nullable: true),
                    Pais = table.Column<string>(type: "varchar(20)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aeroporto",
                columns: table => new
                {
                    Sigla = table.Column<string>(type: "varchar(5)", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(20)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroporto", x => x.Sigla);
                    table.ForeignKey(
                        name: "FK_Aeroporto_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Passageiro",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "varchar(14)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passageiro", x => x.Cpf);
                    table.ForeignKey(
                        name: "FK_Passageiro_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrecoBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrigemSigla = table.Column<string>(type: "varchar(5)", nullable: true),
                    DestinoSigla = table.Column<string>(type: "varchar(5)", nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecoBase_Aeroporto_DestinoSigla",
                        column: x => x.DestinoSigla,
                        principalTable: "Aeroporto",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrecoBase_Aeroporto_OrigemSigla",
                        column: x => x.OrigemSigla,
                        principalTable: "Aeroporto",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinoSigla = table.Column<string>(type: "varchar(5)", nullable: true),
                    OrigemSigla = table.Column<string>(type: "varchar(5)", nullable: true),
                    AeronaveId = table.Column<string>(type: "varchar(10)", nullable: true),
                    HoraEmbarque = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraDesembarque = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voo_Aeronave_AeronaveId",
                        column: x => x.AeronaveId,
                        principalTable: "Aeronave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_Aeroporto_DestinoSigla",
                        column: x => x.DestinoSigla,
                        principalTable: "Aeroporto",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_Aeroporto_OrigemSigla",
                        column: x => x.OrigemSigla,
                        principalTable: "Aeroporto",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Passagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VooId = table.Column<int>(type: "int", nullable: true),
                    PassageiroCpf = table.Column<string>(type: "varchar(14)", nullable: true),
                    ClasseId = table.Column<int>(type: "int", nullable: true),
                    Desconto = table.Column<double>(type: "float", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passagem_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passagem_Passageiro_PassageiroCpf",
                        column: x => x.PassageiroCpf,
                        principalTable: "Passageiro",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passagem_Voo_VooId",
                        column: x => x.VooId,
                        principalTable: "Voo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aeroporto_EnderecoId",
                table: "Aeroporto",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_EnderecoId",
                table: "Passageiro",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Passagem_ClasseId",
                table: "Passagem",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Passagem_PassageiroCpf",
                table: "Passagem",
                column: "PassageiroCpf");

            migrationBuilder.CreateIndex(
                name: "IX_Passagem_VooId",
                table: "Passagem",
                column: "VooId");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoBase_DestinoSigla",
                table: "PrecoBase",
                column: "DestinoSigla");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoBase_OrigemSigla",
                table: "PrecoBase",
                column: "OrigemSigla");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_AeronaveId",
                table: "Voo",
                column: "AeronaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_DestinoSigla",
                table: "Voo",
                column: "DestinoSigla");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_OrigemSigla",
                table: "Voo",
                column: "OrigemSigla");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passagem");

            migrationBuilder.DropTable(
                name: "PrecoBase");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Passageiro");

            migrationBuilder.DropTable(
                name: "Voo");

            migrationBuilder.DropTable(
                name: "Aeronave");

            migrationBuilder.DropTable(
                name: "Aeroporto");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
