using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWatchListWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Watch",
                columns: table => new
                {
                    ReferenceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Movement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BandMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DialColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BraceletColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerReserve = table.Column<double>(type: "float", nullable: true),
                    CaseDiameter = table.Column<double>(type: "float", nullable: true),
                    LugToLugWidth = table.Column<double>(type: "float", nullable: true),
                    Thickness = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watch", x => x.ReferenceNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Watch");
        }
    }
}
