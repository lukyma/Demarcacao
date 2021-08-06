using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace web.api.demarcacao.gestao.usuarios.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "INTERFACE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DESCRICAO = table.Column<string>(type: "text", nullable: false),
                    TAG = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFACE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME = table.Column<string>(type: "text", nullable: false),
                    LOGIN = table.Column<string>(type: "text", nullable: false),
                    PASSWORD = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_INTERFACE",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "bigint", nullable: false),
                    ID_INTERFACE = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_INTERFACE", x => new { x.ID_USUARIO, x.ID_INTERFACE });
                    table.ForeignKey(
                        name: "FK_USUARIO_INTERFACE_INTERFACE_ID_INTERFACE",
                        column: x => x.ID_INTERFACE,
                        principalTable: "INTERFACE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USUARIO_INTERFACE_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "INTERFACE",
                columns: new[] { "ID", "DESCRICAO", "TAG" },
                values: new object[,]
                {
                    { 1L, "Cadastro de Empreendimento", "CAD_EMP" },
                    { 2L, "Atualização de Empreendimento", "ATUAL_EMP" },
                    { 3L, "Exclusão de Empreendimento", "EXC_EMP" },
                    { 4L, "Listagem de Empreendimento", "LIST_EMP" },
                    { 5L, "Cadastro de Terreno", "CAD_TERR" },
                    { 6L, "Atualização de Terreno", "ATUAL_TERR" },
                    { 7L, "Exclusão de Terreno", "EXC_TERR" },
                    { 8L, "Listagem de Terreno", "LIST_TERR" }
                });

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "ID", "LOGIN", "NOME", "PASSWORD" },
                values: new object[,]
                {
                    { 1L, "admin", "Admin", "admin123" },
                    { 2L, "campo", "Campo", "campo123" },
                    { 3L, "cliente", "Cliente", "cliente123" }
                });

            migrationBuilder.InsertData(
                table: "USUARIO_INTERFACE",
                columns: new[] { "ID_INTERFACE", "ID_USUARIO" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 2L, 1L },
                    { 3L, 1L },
                    { 4L, 1L },
                    { 5L, 1L },
                    { 6L, 1L },
                    { 7L, 1L },
                    { 8L, 1L },
                    { 4L, 2L },
                    { 5L, 2L },
                    { 6L, 2L },
                    { 7L, 2L },
                    { 8L, 2L },
                    { 4L, 3L },
                    { 8L, 3L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_INTERFACE_ID_INTERFACE",
                table: "USUARIO_INTERFACE",
                column: "ID_INTERFACE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIO_INTERFACE");

            migrationBuilder.DropTable(
                name: "INTERFACE");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
