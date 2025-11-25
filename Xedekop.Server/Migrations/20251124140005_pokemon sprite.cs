using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xedekop.Server.Migrations
{
    /// <inheritdoc />
    public partial class pokemonsprite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShinySprite",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sprite",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShinySprite",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "Sprite",
                table: "Pokemons");
        }
    }
}
