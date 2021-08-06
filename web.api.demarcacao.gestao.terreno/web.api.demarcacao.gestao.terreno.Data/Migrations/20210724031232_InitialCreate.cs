using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace web.api.demarcacao.gestao.terreno.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TERRENO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_EMPREENDIMENTO = table.Column<long>(type: "bigint", nullable: false),
                    DESCRICAO = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TERRENO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COORDENADA",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_TERRENO = table.Column<long>(type: "bigint", nullable: false),
                    ORDEM = table.Column<int>(type: "integer", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "numeric", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COORDENADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COORDENADA_TERRENO_ID_TERRENO",
                        column: x => x.ID_TERRENO,
                        principalTable: "TERRENO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COORDENADA_ID_TERRENO",
                table: "COORDENADA",
                column: "ID_TERRENO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COORDENADA");

            migrationBuilder.DropTable(
                name: "TERRENO");
        }
    }
}
