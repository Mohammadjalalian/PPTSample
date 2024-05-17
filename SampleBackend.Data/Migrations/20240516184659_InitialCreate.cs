using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.InsertData(
                               table: "Images",
                               columns: new[] { "Id", "ImageUrl" },
                               values: new object[,]
                               {
                                   {1, "https://api.dicebear.com/8.x/pixel-art/png?seed=1&size=150"},
                                   {2, "https://api.dicebear.com/8.x/pixel-art/png?seed=2&size=150"},
                                   {3, "https://api.dicebear.com/8.x/pixel-art/png?seed=3&size=150"},
                                   {4, "https://api.dicebear.com/8.x/pixel-art/png?seed=4&size=150"},
                                   {5, "https://api.dicebear.com/8.x/pixel-art/png?seed=5&size=150"},
                               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
