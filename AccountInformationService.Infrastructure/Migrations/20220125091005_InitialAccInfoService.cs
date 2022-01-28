using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountInformationService.Infrastructure.Migrations
{
    public partial class InitialAccInfoService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientAccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<string>(nullable: true),
                    CustodianId = table.Column<string>(nullable: true),
                    CustodianName = table.Column<string>(nullable: true),
                    RegisteredName = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CustodialAccountNumber = table.Column<string>(nullable: true),
                    MarketValue = table.Column<string>(nullable: true),
                    ProgramId = table.Column<string>(nullable: true),
                    ProgramName = table.Column<string>(nullable: true),
                    IsClosed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientID = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ClientType = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAccountDetails");

            migrationBuilder.DropTable(
                name: "ClientDetails");
        }
    }
}
