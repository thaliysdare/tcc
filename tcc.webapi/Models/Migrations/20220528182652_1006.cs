using Microsoft.EntityFrameworkCore.Migrations;

namespace tcc.webapi.Migrations
{
    public partial class _1006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo",
                column: "Placa",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo");
        }
    }
}
