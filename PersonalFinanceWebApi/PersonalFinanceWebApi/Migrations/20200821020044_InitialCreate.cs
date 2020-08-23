using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalFinanceWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Balance = table.Column<float>(nullable: false),
                    LatestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    TransDate = table.Column<DateTime>(nullable: false),
                    amount = table.Column<float>(nullable: false),
                    TransType = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountItems");

            migrationBuilder.DropTable(
                name: "TransactionItems");
        }
    }
}
