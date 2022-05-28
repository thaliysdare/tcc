using Microsoft.EntityFrameworkCore.Migrations;

namespace tcc.webapi.Migrations
{
    public partial class _1005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionalidade_Usuario_UsuarioId",
                table: "Funcionalidade");

            migrationBuilder.DropIndex(
                name: "IX_Funcionalidade_UsuarioId",
                table: "Funcionalidade");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Funcionalidade");

            migrationBuilder.AddColumn<int>(
                name: "Nivel",
                table: "Funcionalidade",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Funcionalidade");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Funcionalidade",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionalidade_UsuarioId",
                table: "Funcionalidade",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionalidade_Usuario_UsuarioId",
                table: "Funcionalidade",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
