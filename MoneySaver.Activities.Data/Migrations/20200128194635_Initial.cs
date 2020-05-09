using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneySaver.Activities.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateOn = table.Column<DateTime>(nullable: false),
                    ModifyOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MarketId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    AdditionalNote = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
