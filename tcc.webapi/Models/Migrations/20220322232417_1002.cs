using Microsoft.EntityFrameworkCore.Migrations;

namespace tcc.webapi.Migrations
{
    public partial class _1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Endereco",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Endereco");
        }
    }
}
